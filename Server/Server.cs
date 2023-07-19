using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using LiteNetLib;
using ULTRANET.Core;
using ULTRANET.Core.Networking;

namespace ULTRANET.Server
{
    public static class Server
    {
        public static async Task Initialize(int port)
        {
            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager server = new NetManager(listener);
            server.Start(port);

            Console.WriteLine($"Server started on port {port}!");

            ServerHandler handler = new ServerHandler(server, listener); 
            while (server.IsRunning)
            {
                server.PollEvents();
                await Task.Delay(15);
            }
        }
    }
}