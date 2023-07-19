using System;

namespace ULTRANET.Core.Networking
{
    [Serializable]
    public enum PacketType
    {
        Connect,
        Disconnect,

        // ROOM
        SwitchRoom,

        // Error
        Error
    }

    [Serializable]
    public struct Packet<T>
    {
        public PacketType Type { get; set; }
        public T Data { get; set; }

        public byte[] RawData()
        {
            return Serializer.Serialize(Data);
        }
    }
}