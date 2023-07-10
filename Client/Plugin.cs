using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BepInEx.Logging;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using UnityEngine;
using ULTRANET.Core;
using UMM;
using UnityEngine.SceneManagement;

namespace ULTRANET.Client
{
    [UKPlugin(pluginGuid, pluginName, pluginVersion, "ULTRAKILL Multiplayer", false, true)]
    public class Plugin : UKMod
    {
        public const string pluginGuid = "com.ultranet.client";
        public const string pluginName = "ULTRANET Client";
        public const string pluginVersion = "0.1.0";

        private static ManualLogSource _logger;
        private static string _ip;
        private static int _port;
        
        public static IChannel Channel;

        public static int PlayerID = 0;
        public static bool IsConnected = false;
        
        private void Crash()
        {
            Environment.Exit(1);
        }

        private void Awake()
        {
            // Create logger
            _logger = BepInEx.Logging.Logger.CreateLogSource("ULTRANET");

            // Disable Cybergrind Submission
            UMM.UKAPI.DisableCyberGrindSubmission("ULTRANET BLOCKED CYBERGRIND SUBMISSION");
            _logger.LogInfo("ULTRANET Client Loaded.");
            
            if(!PersistentModDataExists("ip"))
                SetPersistentModData("ip", "127.0.0.1", pluginName);
            
            if(!PersistentModDataExists("port"))
                SetPersistentModData("port", "1234", pluginName);
            
            _ip = RetrieveStringPersistentModData("ip", pluginName);
            _port = RetrieveIntPersistentModData("port");

            _logger.LogInfo("ULTRANET Client Initialized.");
            
            // On SceneChange
            SceneManager.sceneLoaded += OnSceneChange;
        }

        private void OnSceneChange(Scene scene, LoadSceneMode loadMode)
        {
            if (!IsConnected)
                return;
            
            VariablePacket send = new VariablePacket(NetworkKeys.ROOM_CHANGE, PlayerID, 
                SceneHelper.CurrentScene);
            
            NetworkHelpers.SendPacket(Channel, send);
        }

        private void OnGUI()
        {
            // UI
            GUI.Box(new Rect(10, 10, 220, 150), "ULTRANET");

            if (UMM.UKAPI.InLevel() && !IsConnected)
            {
                // Show error
                GUI.Label(new Rect(20, 40, 100, 20), "Please connect to a ULTRANET server in the Main Menu.");
                return;
            }

            if (!IsConnected)
            {
                GUI.Label(new Rect(20, 40, 100, 20), "IP:");
                _ip = GUI.TextField(new Rect(60, 40, 160, 20), _ip);

                GUI.Label(new Rect(20, 60, 100, 20), "Port:");
                _port = int.Parse(GUI.TextField(new Rect(60, 60, 160, 20), _port.ToString()));
            }

            if (PlayerID != 0)
            {
                GUI.Label(new Rect(20, 120, 100, 20), $"Player ID: {PlayerID}");
                return;
            }
            
            if (GUI.Button(new Rect(20, 90, 100, 30), "Connect"))
            {
                Thread connectionThread = new Thread(ConnectionLoop);
                connectionThread.Start();
            }
        }

        private void ConnectionLoop()
        {
            HudMessageReceiver.Instance.SendHudMessage($"Attempting to connect to ULTRANET Server at {_ip}:{_port}...");
            var group = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new DotNetty.Transport.Bootstrapping.Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new StringEncoder(), new StringDecoder(), new ClientHandler());
                    }));

                IChannel clientChannel = bootstrap.ConnectAsync(_ip, _port).Result;
                string connectProtocol = new VariablePacket(NetworkKeys.Client.CONNECT, 0, "");
                NetworkHelpers.SendPacket(clientChannel, connectProtocol);
                
                // Wait 5 seconds for connection
                Thread.Sleep(5000);

                if (PlayerID == 0)
                {
                    HudMessageReceiver.Instance.SendHudMessage(
                        $"Failed to connect to ULTRANET Server at {_ip}:{_port}.");
                    clientChannel.CloseAsync();
                }

                while (IsConnected) {}
            }
            finally
            {
                group.ShutdownGracefullyAsync();
            }
        }

        private void OnApplicationQuit()
        {
            if (Channel != null)
            {
                VariablePacket send = new VariablePacket(NetworkKeys.Client.DISCONNECT, PlayerID, "");
                NetworkHelpers.SendPacket(Channel, send);
                Channel.CloseAsync();
            }
        }
    }

    internal class ClientHandler : SimpleChannelInboundHandler<string>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            Plugin.Channel = ctx.Channel;
            
            // TODO: Use Switch Statement
            Console.WriteLine($"Received from server: {msg}");
            VariablePacket packet = msg;
            
            if (packet.key == NetworkKeys.ERROR)
            {
                HudMessageReceiver.Instance.SendHudMessage($"ERROR: <color=red>{packet.data}</color>");
                return;
            }
            
            if (packet.key == NetworkKeys.PLAYER_ID)
            {
                Plugin.PlayerID = packet.entity;
                Console.WriteLine($"PLAYER ID: {packet.entity}");
                
                HudMessageReceiver.Instance.SendHudMessage($"Connected to ULTRANET Server!\nPlayer ID: {packet.entity}");
                Plugin.IsConnected = true;
                return;
            }

            if (packet.key == NetworkKeys.ROOM_CHANGE)
            {
                HudMessageReceiver.Instance.SendHudMessage($"{packet.entity} joined \"{packet.data}\"");
                return;
            }
            
            if(packet.key == NetworkKeys.MESSAGE)
            {
                HudMessageReceiver.Instance.SendHudMessage(packet.data);
                return;
            }
        }
    }
}