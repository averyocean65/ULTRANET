using System.Threading;
using System.Threading.Tasks;
using BepInEx.Logging;
using LiteNetLib;

namespace ULTRANET.Client.Networking
{
    public class Client
    {
        public static async Task TryConnect(string ip, ushort port, string username)
        {
            ManualLogSource logger = Logger.CreateLogSource("ULTRANET.NetworkHandler");
            logger.LogInfo("Attempting Connection...");

            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager client = new NetManager(listener);

            client.Start();
            client.Connect(ip, port, "ULTRANET");

            ClientHandler handler = new ClientHandler(client, listener, logger);

            while (true)
            {
                handler.PollEvents();
                Thread.Sleep(15);
            }
        }
    }
}