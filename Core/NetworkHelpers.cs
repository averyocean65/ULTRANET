using System;
using System.Collections.Concurrent;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace ULTRANET.Core
{
    public struct VariablePacket
    {
        public string key;
        public int entity;
        public string data;

        public byte[] dataBytes
        {
            get => Encoding.UTF8.GetBytes(data);
            set => data = Encoding.UTF8.GetString(value);
        }
        
        public VariablePacket(string key, int entity, string data)
        {
            this.key = key;
            this.entity = entity;
            this.data = data;
        }

        public override string ToString() => (string)this;
        public byte[] ToBytes() => (byte[])this;

        public static implicit operator string(VariablePacket data)
        {
            string fullKey = data.key.PadRight(8, ' ');
            string fullEntity = data.entity.ToString().PadRight(4, ' ');
            
            return fullKey +
                   fullEntity +
                   data.data;
        }

        public static implicit operator byte[](VariablePacket data)
        {
            return Encoding.UTF8.GetBytes((string)data);
        }
        
        public static implicit operator VariablePacket(string str)
        {
            return (VariablePacket)Encoding.UTF8.GetBytes(str);
        }
        
        public static implicit operator VariablePacket(byte[] data)
        {
            VariablePacket nData = new VariablePacket();
            
            // First 8 bytes are the key
            string fullStr = Encoding.UTF8.GetString(data);
            string key = fullStr.Substring(0, 8);
            nData.key = key.Trim();

            try
            {
                // The next 4 bytes are the entity
                string entityStr = fullStr.Substring(8, 4);
                nData.entity = int.Parse(entityStr.Trim());

                // The rest is the data
                string dataStr = fullStr.Substring(12);
                nData.data = dataStr;
            }
            catch
            {
                nData.data = "";
                nData.entity = 0;
            }

            return nData;
        }

        public static bool operator ==(VariablePacket a, VariablePacket b)
        {
            return a.key == b.key &&
                   a.entity == b.entity &&
                   a.data == b.data;
        }

        public static bool operator !=(VariablePacket a, VariablePacket b)
        {
            return !(a == b);
        }
    }
    
    public static class NetworkHelpers
    {
        public static void SendPacket(DotNetty.Transport.Channels.IChannel channel, byte[] bytes)
        {
            IByteBuffer buffer = Unpooled.WrappedBuffer(bytes); 
            channel.WriteAndFlushAsync(buffer);
        }
        
        public static void SendPacket(DotNetty.Transport.Channels.IChannel channel, VariablePacket packet)
        {
            packet.key = packet.key.PadRight(8, ' ');
            SendPacket(channel, packet.ToBytes());
            
            Console.WriteLine($"SENT PACKET:\t{packet}");
        }

        public static void SendPacketAll(ConcurrentDictionary<IChannelId, DotNetty.Transport.Channels.IChannel> channels,
            VariablePacket packet)
        {
            foreach (var client in channels)
            {
                client.Value.WriteAndFlushAsync(
                    Unpooled.WrappedBuffer(Encoding.UTF8.GetBytes(packet))
                );
            }
        }
    }
}