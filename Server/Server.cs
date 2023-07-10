using System;
using System.Collections.Concurrent;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using ULTRANET.Core;

namespace ULTRANET.Server
{
    public static class Server
    {
        public static int NextPlayerID = 1;
        public static int PlayerCount = 0;

        public static async Task Initialize(IPEndPoint ip)
        {
            var bossGroup = new MultithreadEventLoopGroup(1);
            var workerGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler("SRV-LSTN"))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new StringEncoder(), new StringDecoder(), new ServerHandler());
                    }));

                IChannel serverChannel = await bootstrap.BindAsync(ip);
                // Console.WriteLine("Server started and listening on " + ip);

                await serverChannel.CloseCompletion;
            }
            finally
            {
                await Task.WhenAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }
    }

    class ServerHandler : SimpleChannelInboundHandler<string>
    {
        private static readonly ConcurrentDictionary<IChannelId, IChannel> Channels = new ConcurrentDictionary<IChannelId, IChannel>();
        
        private void HandlePacket(VariablePacket packet, IChannel ctx)
        {
            Console.WriteLine("Handling packet...");

            VariablePacket data = new VariablePacket();
            
            if (packet.key == NetworkKeys.Client.CONNECT)
            {
                if (Server.PlayerCount > 10)
                {
                    data = new VariablePacket(NetworkKeys.ERROR, 0, "Server is full!");
                    NetworkHelpers.SendPacket(ctx, data);
                    return;
                }
                
                string playerID = new VariablePacket(NetworkKeys.PLAYER_ID, Server.NextPlayerID, "");
                Server.NextPlayerID++;
                Server.PlayerCount++;
                
                NetworkHelpers.SendPacket(ctx, playerID);
                return;
            }

            if (packet.key == NetworkKeys.Client.DISCONNECT)
            {
                Console.WriteLine($"{packet.entity} DISCONNECTED!");
                Server.PlayerCount--;
                return;
            }

            if (packet.key == NetworkKeys.PING)
            {
                Console.WriteLine($"{packet.entity} PINGED!");
                
                data = new VariablePacket(NetworkKeys.PING, packet.entity, "PONG");
                NetworkHelpers.SendPacket(ctx, data);
                return;
            }

            if (packet.key == NetworkKeys.MESSAGE)
            {
                Console.WriteLine($"MESSAGE FROM {packet.entity}: {packet.data}");
                packet.data = $"{packet.entity}: {packet.data.Trim()}";
                NetworkHelpers.SendPacketAll(Channels, packet);
            }
            
            if (packet.key == NetworkKeys.ROOM_CHANGE)
            {
                data = packet;
                NetworkHelpers.SendPacketAll(Channels, data);
                return;
            }

            data = new VariablePacket(NetworkKeys.ERROR, 0, "Invalid packet key!");
            NetworkHelpers.SendPacket(ctx, data);
        }
        
        protected override void ChannelRead0(IChannelHandlerContext ctx, string message)
        {
            Console.WriteLine("Received message from client: " + message);

            // Process the received message
            VariablePacket packet = (VariablePacket)message;
            string packetDataStr = packet.data;
            Console.Write($"PACKET DATA:\nKEY:\t {packet.key}\nPLAYER:\t {packet.entity}\nDATA:\t {packetDataStr}\n------\n");
            
            // Handle Packet
            HandlePacket(packet, ctx.Channel);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            Channels.TryAdd(context.Channel.Id, context.Channel);
            base.ChannelActive(context);
        }
        
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Channels.TryRemove(context.Channel.Id, out _);
            base.ChannelInactive(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}