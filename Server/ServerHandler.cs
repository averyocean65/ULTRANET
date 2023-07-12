// #define LOG_TRANSFORM_UPDATES

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
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

        private static int NextPlayerId { get; set; } = 0;

        private void SendPacketToAllExcept(DynamicPacket packet, IChannelId id)
        {
            foreach (var channel in Channels)
            {
                if (Equals(channel.Key, id))
                    continue;

                PacketHandler.SendPacket(channel.Value, packet.ToByteArray());
            }
        }

        private void SendPacketToAll(DynamicPacket packet)
        {
            foreach (var channel in Channels)
            {
                PacketHandler.SendPacket(channel.Value, packet.ToByteArray());
            }
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, string message)
        {
            _channel = ctx.Channel;

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                DynamicPacket packet = DynamicPacket.Parser.ParseFrom(data);

                PacketFlag flags = PacketFlagConverter.FromUInt32(packet.Flags);

                PacketHandler.PacketEvent(packet, ProtocolHeaders.CONNECT, OnPlayerConnect);
                PacketHandler.PacketEvent(packet, ProtocolHeaders.MESSAGE, OnPlayerMessage);
                PacketHandler.PacketEvent(packet, ProtocolHeaders.GET_PLAYER, OnGetPlayer);
                PacketHandler.PacketEvent(packet, ProtocolHeaders.CHANGE_ROOM, OnPlayerRoomChange);
                PacketHandler.PacketEvent(packet, ProtocolHeaders.PLAYER_TRANSFORM_UPDATE, OnPlayerTransformUpdate);
            }
            catch (Exception ex)
            {
                // Ignore. This is usually caused by a malformed packet.
                Console.Error.WriteLine($"EXCEPTION: {ex.Message}");
            }
        }

        private void OnPlayerTransformUpdate(DynamicPacket packet, PacketFlag flags)
        {
            // Deserialize the value field into a Transform message
            string[] valueStrings = packet.Value.ToStringUtf8().Split(';');

            // Parse Doubles with InvariantCulture
            double[] values = valueStrings.Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToArray();

            Transform transform = new Transform()
            {
                PosX = values[0], PosY = values[1], PosZ = values[2],
                RotX = values[3], RotY = values[4], RotZ = values[5],
                SclX = values[6], SclY = values[7], SclZ = values[8]
            };

            // Access the transform data
            var transformData = TransformUtils.FromTransform(transform);

            // Handle the transform update
            // TODO: Implement your logic here
            // You can access individual values like transformData[0] for Position.x, transformData[1] for Position.y, etc.

            // For example, print the transform data
            Player player = Players[packet.PlayerId];

#if LOG_TRANSFORM_UPDATES
            Console.WriteLine($"Received transform update from {player.Name}:");
            Console.WriteLine("Position:\t({0}, {1}, {2})", transform.PosX, transform.PosY, transform.PosZ);
            Console.WriteLine("Rotation:\t({0}, {1}, {2})", transform.RotX, transform.RotY, transform.RotZ);
            Console.WriteLine("Scale:\t\t({0}, {1}, {2})", transform.SclX, transform.SclY, transform.SclZ);
#endif
        }

        private void OnPlayerRoomChange(DynamicPacket arg1, PacketFlag arg2)
        {
            // Get scene name
            string scene = arg1.Value.ToStringUtf8();

            // Player
            Player player = Players[arg1.PlayerId];
            player.Room = scene;

            // Send message to all players
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.CHANGE_ROOM, arg1.PlayerId,
                PacketFlag.None, ByteString.CopyFromUtf8(scene));

            Console.WriteLine($"Player {player.Name} ({player.Id}) changed room to {scene}");
            SendPacketToAll(packet);
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
            SendPacketToAll(arg1);
        }

        private void OnPlayerConnect(DynamicPacket obj, PacketFlag flags)
        {
            // Get player from Packet Data
            Player player = Player.Parser.ParseFrom(obj.Value.ToByteArray());
            player.Id = (uint)NextPlayerId;
            int nextID = NextPlayerId;

            // Increment player id
            NextPlayerId++;

            // Cache player
            Players.TryAdd((uint)nextID, player);
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