using System.Threading.Tasks;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Google.Protobuf;
using ULTRANET.Core;
using ULTRANET.Core.Protobuf;

namespace ULTRANET.Client.Networking
{
    public class NetworkHandler
    {
        public static async Task TryConnect(string ip, ushort port, string username)
        {
            var group = new MultithreadEventLoopGroup();

            try
            {
                // Create Bootstrap object
                var bootstrap = new DotNetty.Transport.Bootstrapping.Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new StringEncoder(), new StringDecoder(), new ClientHandler());
                    }));

                // Get communication channel
                IChannel clientChannel = await bootstrap.ConnectAsync(ip, port);

                // Connection Packet
                Player player = new Player();
                player.Name = username;
                player.Room = "";
                player.Id = 0;

                // Wait 100ms for connection to be established
                await Task.Delay(100);

                DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.CONNECT, 0, PacketFlag.None,
                    player.ToByteString());

                byte[] connectProtocol = packet.ToByteArray();
                PacketHandler.SendPacket(clientChannel, connectProtocol);

                Plugin.Connected = true;

                // Wait for connection to close
                await clientChannel.CloseCompletion;
            }
            finally
            {
                await group.ShutdownGracefullyAsync();
            }
        }

        public static void TryDisconnect(IChannel clientChannel)
        {
            Plugin.Connected = false;
            clientChannel.CloseAsync();
        }
    }
}