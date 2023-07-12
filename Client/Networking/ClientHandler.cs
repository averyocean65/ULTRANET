using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using BepInEx.Logging;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using ULTRANET.Core;
using ULTRANET.Core.Protobuf;

namespace ULTRANET.Client.Networking
{
    public class ClientHandler : SimpleChannelInboundHandler<string>
    {
        private static readonly ConcurrentDictionary<uint, Player> _players = new ConcurrentDictionary<uint, Player>();
        public static IChannel Channel;

        public static ManualLogSource Logger;
        public static Player LocalPlayer { get; private set; }

        /// <summary>
        /// Called when a message is received from the server.
        /// </summary>
        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            Channel = ctx.Channel;
            // Parse to DynPacket
            byte[] data = Encoding.UTF8.GetBytes(msg);
            DynamicPacket packet = DynamicPacket.Parser.ParseFrom(data);

            // Get packet flags
            PacketFlag flags = PacketFlagConverter.FromUInt32(packet.Flags);

            // Handlers
            PacketHandler.PacketEvent(packet, ProtocolHeaders.SET_LOCAL_PLAYER, SetLocalPlayerHandler);
            PacketHandler.PacketEvent(packet, ProtocolHeaders.MESSAGE, MessageHandler);
            PacketHandler.PacketEvent(packet, ProtocolHeaders.GET_PLAYER, PlayerHandler);
            PacketHandler.PacketEvent(packet, ProtocolHeaders.CHANGE_ROOM, ChangeRoomHandler);
        }

        private void ChangeRoomHandler(DynamicPacket arg1, PacketFlag arg2)
        {
            Player player = GetPlayer(arg1.PlayerId);
            if (player == null)
                return;

            player.Room = arg1.Value.ToStringUtf8();

            HudMessageReceiver.Instance.SendHudMessage(
                newmessage: $"Player <color=yellow>{player.Name}</color> switched to {arg1.Value.ToStringUtf8()}",
                silent: false
            );
        }

        private void SetLocalPlayerHandler(DynamicPacket obj, PacketFlag flags)
        {
            // Get player from Packet Data
            Player player = Player.Parser.ParseFrom(obj.Value.ToByteArray());

            // Cache player
            _players[player.Id] = player;

            // Set local player
            LocalPlayer = player;

            Logger.LogInfo($"Local Player set to {LocalPlayer.Name} ({LocalPlayer.Id})");
        }

        private void PlayerHandler(DynamicPacket obj, PacketFlag flags)
        {
            // Get player from Packet Data
            Player player = Player.Parser.ParseFrom(obj.Value.ToByteArray());

            // Cache player
            _players[obj.PlayerId] = player;
        }

        private void MessageHandler(DynamicPacket obj, PacketFlag flags)
        {
            Player player = GetPlayer(obj.PlayerId);
            if (player == null)
            {
                HudMessageReceiver.Instance.SendHudMessage(
                    newmessage: $"[Unknown]: {obj.Value.ToStringUtf8()}",
                    silent: true
                );
            }

            // Print message to HUD
            HudMessageReceiver.Instance.SendHudMessage(
                newmessage: $"[{player.Name}]: {obj.Value.ToStringUtf8()}",
                silent: true
            );
        }

        private Player GetPlayer(uint playerId)
        {
            // Get cached player
            Player player = _players[playerId];

            if (player == null)
            {
                // Fetch player from Server
                DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.GET_PLAYER, playerId,
                    PacketFlag.None, "");

                PacketHandler.SendPacket(Channel, packet.ToByteArray());

                // Wait for player to be cached (maxes out at a certain number of iterations)
                int iterMax = 500; // 10ms * 500 = 5s
                int i = 0;
                while (player == null && i < iterMax)
                {
                    player = _players[playerId];
                    Thread.Sleep(10);

                    i++;
                }
            }

            return player;
        }
    }
}