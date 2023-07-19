using System;
using BepInEx.Logging;
using JetBrains.Annotations;
using LiteNetLib;
using ULTRANET.Core;
using ULTRANET.Core.Networking;

namespace ULTRANET.Client.Networking
{
    public class ClientHandler : PacketHandler
    {
        private ManualLogSource _logger;

        public ClientHandler([NotNull] NetManager manager, [NotNull] EventBasedNetListener listener,
            ManualLogSource logger)
            : base(true, manager, listener)
        {
            if (logger == null)
            {
                _logger = BepInEx.Logging.Logger.CreateLogSource("ULTRANET.ClientHandler");
                return;
            }

            _logger = logger;
        }

        protected override void OnConnectPacket(NetPeer peer, ConnectionData data)
        {
            _logger.LogInfo($"User {data.Player.Name} connected!");
            HudMessageReceiver.Instance.SendHudMessage(
                $"<color=yellow>{data.Player.Name}</color> joined the game!"
            );
        }

        protected override void OnDisconnect(NetPeer peer, DisconnectionData data)
        {
            _logger.LogInfo($"User {data.Player.Name} disconnected!");
            HudMessageReceiver.Instance.SendHudMessage(
                $"<color=yellow>{data.Player.Name}</color> left the game!"
            );
        }

        protected override void OnMiscPacket(NetPeer peer, Packet<object> objectPacket)
        {
            throw new NotImplementedException();
        }

        protected override void OnErrorPacket(NetPeer peer, string error)
        {
            _logger.LogError($"Error: {error}");
        }

        protected override void OnException(Exception exception)
        {
            _logger.LogFatal($"Exception: {exception}");
        }
    }
}