using LiteNetLib;
using LiteNetLib.Utils;
using ULTRANET.Core.Networking;

namespace ULTRANET.Core
{
    public static class PacketUtils
    {
        public static void SendPacket<T>(this NetPeer peer, Packet<T> packet)
        {
            Packet<object> objPacket = new Packet<object>
            {
                Type = packet.Type,
                Data = packet.Data ?? new object()
            };

            NetDataWriter writer = new NetDataWriter();
            writer.Put(Serializer.Serialize(objPacket));
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
        }
    }
}