using System;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using ULTRANET.Core.Protobuf;

namespace ULTRANET.Core
{
    public class PacketHandler
    {
        public static void SendPacket(IChannel channel, byte[] data)
        {
            IByteBuffer buffer = Unpooled.Buffer(data.Length);
            buffer.WriteBytes(data);

            channel.WriteAndFlushAsync(buffer);
        }

        public static void SendPacketToChannels(IChannel[] channels, byte[] data)
        {
            IByteBuffer buffer = Unpooled.Buffer(data.Length);
            buffer.WriteBytes(data);

            foreach (IChannel channel in channels)
            {
                channel.WriteAndFlushAsync(buffer);
            }
        }

        public static void PacketEvent(DynamicPacket packet, string key, Action<DynamicPacket, PacketFlag> action)
        {
            if (packet.Key == key)
            {
                // Get packet flags
                PacketFlag flags = PacketFlagConverter.FromUInt32(packet.Flags);

                action(packet, flags);
            }
        }

        public static DynamicPacket GeneratePacket(string key, uint playerID, PacketFlag flags, string data)
        {
            return GeneratePacket(key, playerID, flags, ByteString.CopyFromUtf8(data));
        }

        public static DynamicPacket GeneratePacket(string key, uint playerID, PacketFlag flags, ByteString data)
        {
            DynamicPacket packet = new DynamicPacket();
            packet.Key = key;
            packet.PlayerId = playerID;
            packet.Flags = PacketFlagConverter.ToUInt32(flags);
            packet.Value = data;

            return packet;
        }

        public static DynamicPacket ToPacket(byte[] data)
        {
            return DynamicPacket.Parser.ParseFrom(data);
        }
    }
}