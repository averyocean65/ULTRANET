using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using LiteNetLib;
using ULTRANET.Core;
using ULTRANET.Core.Networking;
using UnityEngine;

namespace ULTRANET.Server
{
    public class ServerHandler : PacketHandler
    {
        private static int NextID = 0;
        
        public ServerHandler([NotNull] NetManager manager, [NotNull] EventBasedNetListener listener) : base(false, manager, listener)
        { }

        protected override void OnConnectPacket(NetPeer peer, ConnectionData data)
        {
            int id = NextID++;
            Console.WriteLine($"{data.Player.Name} ({id}) connected!");
            
            // Create a copy
            ConnectionData send = new ConnectionData
            {
                Player = new Player()
                {
                    Name = data.Player.Name,
                    Id = id,
                    Room = ""
                }
            };
            
            // Send the copy to the client
            Packet<ConnectionData> packet = new Packet<ConnectionData>
            {
                Type = PacketType.Connect,
                Data = send
            };
            
            peer.Send(Serializer.Serialize(packet), DeliveryMethod.ReliableOrdered);
        }

        protected override void OnDisconnect(NetPeer peer, DisconnectionData data)
        {
            Console.WriteLine($"{data.Player.Name} disconnected!");
        }

        protected override void OnMiscPacket(NetPeer peer, Packet<object> objectPacket)
        {
            throw new Exception("idfk what to do help");
        }

        protected override void OnErrorPacket(NetPeer peer, string error)
        {
            Console.Error.WriteLine($"Error: {error}");
        }

        protected override void OnException(Exception exception)
        {
            Console.Error.WriteLine($"Exception: {exception}");
        }

        protected override void OnConnectRequest(ConnectionRequest request)
        {
            base.OnConnectRequest(request);
            if(_manager.ConnectedPeersCount < 10)
                request.AcceptIfKey("ULTRANET");
            else
                request.Reject();
        }
    }
}