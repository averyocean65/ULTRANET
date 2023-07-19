using System;
using LiteNetLib;
using ULTRANET.Core.Networking;

namespace ULTRANET.Core
{
    public abstract class PacketHandler
    {
        protected EventBasedNetListener _listener;
        protected NetManager _manager;

        public PacketHandler(bool isClient, NetManager manager, EventBasedNetListener listener)
        {
            _manager = manager;
            _listener = listener;

            if (!isClient)
                _listener.ConnectionRequestEvent += OnConnectRequest;

            _listener.PeerConnectedEvent += OnConnect;
            _listener.PeerDisconnectedEvent += OnDisconnect;
            _listener.NetworkReceiveEvent += HandlePacket;
        }

        protected abstract void OnConnectPacket(NetPeer peer, ConnectionData data);
        protected abstract void OnDisconnect(NetPeer peer, DisconnectionData data);
        protected abstract void OnMiscPacket(NetPeer peer, Packet<object> objectPacket);
        protected abstract void OnErrorPacket(NetPeer peer, string error);

        protected abstract void OnException(Exception exception);

        public virtual void PollEvents()
        {
            _manager.PollEvents();
        }

        protected virtual void HandlePacket(NetPeer peer, NetPacketReader reader, byte channel,
            DeliveryMethod deliveryMethod)
        {
            try
            {
                Console.WriteLine("Received packet!");

                Packet<object> objectPacket = Serializer.Deserialize<Packet<object>>(reader.GetRemainingBytes());
                Console.WriteLine("Packet Type: " + objectPacket.Type);

                switch (objectPacket.Type)
                {
                    case PacketType.Connect:
                        ConnectionData data = (ConnectionData)objectPacket.Data;
                        OnConnectPacket(peer, data);
                        break;
                    case PacketType.Disconnect:
                        DisconnectionData disconnectionData = (DisconnectionData)objectPacket.Data;
                        OnDisconnect(peer, disconnectionData);
                        break;

                    case PacketType.Error:
                        string error = (string)objectPacket.Data;
                        OnErrorPacket(peer, error);
                        break;

                    default:
                        OnMiscPacket(peer, objectPacket);
                        break;
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }


        protected virtual void OnDisconnect(NetPeer peer, DisconnectInfo disconnectInfo)
        {
        }

        // These functions are virtual and empty, because they might not have to be implemented in every packet handler.
        // Example: OnConnectRequest doesn't have to be implemented by the client, only by the server.
        protected virtual void OnConnectRequest(ConnectionRequest request)
        {
        }

        protected virtual void OnConnect(NetPeer peer)
        {
        }
    }
}