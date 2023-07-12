using System;
using System.Collections.Concurrent;
using System.Text;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using ULTRANET.Core;
using ULTRANET.Core.Protobuf;

namespace ULTRANET.Server
{
    class ServerHandler : SimpleChannelInboundHandler<string>
    {
        private static readonly ConcurrentDictionary<uint, Player> Players = new ConcurrentDictionary<uint, Player>();

        private static readonly ConcurrentDictionary<IChannelId, IChannel> Channels =
            new ConcurrentDictionary<IChannelId, IChannel>();

        private IChannel _channel;

        protected override void ChannelRead0(IChannelHandlerContext ctx, string message)
        {
            _channel = ctx.Channel;

            byte[] data = Encoding.UTF8.GetBytes(message);
            DynamicPacket packet = DynamicPacket.Parser.ParseFrom(data);

            PacketFlag flags = PacketFlagConverter.FromUInt32(packet.Flags);

            PacketHandler.PacketEvent(packet, ProtocolHeaders.CONNECT, OnPlayerConnect);
            PacketHandler.PacketEvent(packet, ProtocolHeaders.MESSAGE, OnPlayerMessage);
            PacketHandler.PacketEvent(packet, ProtocolHeaders.GET_PLAYER, OnGetPlayer);
            PacketHandler.PacketEvent(packet, ProtocolHeaders.CHANGE_ROOM, OnPlayerRoomChange);
        }

        private void OnPlayerRoomChange(DynamicPacket arg1, PacketFlag arg2)
        {
            // Get scene name
            string scene = arg1.Value.ToStringUtf8();

            // Player
            Player player = Players[arg1.PlayerId];

            // Send message to all players
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.CHANGE_ROOM, arg1.PlayerId,
                PacketFlag.None, ByteString.CopyFromUtf8(scene));

            Console.WriteLine($"Player {player.Name} ({packet.PlayerId}) changed room to {scene}");

            foreach (var channel in Channels)
            {
                PacketHandler.SendPacket(channel.Value, packet.ToByteArray());
            }
        }

        private void OnGetPlayer(DynamicPacket arg1, PacketFlag arg2)
        {
            // Get player from cache
            Player player = Players[arg1.PlayerId];

            // Send player to client
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.GET_PLAYER, arg1.PlayerId,
                PacketFlag.None, player.ToByteString());

            // Send packet
            PacketHandler.SendPacket(_channel, packet.ToByteArray());
        }

        private void OnPlayerMessage(DynamicPacket arg1, PacketFlag arg2)
        {
            // Send message to all players
            foreach (var channel in Channels)
            {
                PacketHandler.SendPacket(channel.Value, arg1.ToByteArray());
            }
        }

        private void OnPlayerConnect(DynamicPacket obj, PacketFlag flags)
        {
            // Get player from Packet Data
            Player player = Player.Parser.ParseFrom(obj.Value.ToByteArray());

            // Cache player
            Players[obj.PlayerId] = player;
            Console.WriteLine("Player connected: " + player.Name);

            // Generate packet
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.SET_LOCAL_PLAYER, obj.PlayerId,
                PacketFlag.None, player.ToByteString());

            // Send packet
            PacketHandler.SendPacket(_channel, packet.ToByteArray());
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);

            Channels.TryAdd(context.Channel.Id, context.Channel);
            Console.WriteLine("Client connected: " + context.Channel.RemoteAddress);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);

            Channels.TryRemove(context.Channel.Id, out _);
            Console.WriteLine("Client disconnected: " + context.Channel.RemoteAddress);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}