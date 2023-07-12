using System.Threading;
using BepInEx.Logging;
using Google.Protobuf;
using ULTRANET.Client.Networking;
using ULTRANET.Core;
using ULTRANET.Core.Protobuf;
using UMM;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ULTRANET.Client
{
    [UKPlugin(pluginGuid, pluginName, pluginVersion, "ULTRAKILL Multiplayer", false, true)]
    public class Plugin : UKMod
    {
        public const string pluginGuid = "com.ultranet.client";
        public const string pluginName = "ULTRANET Client";
        public const string pluginVersion = "0.1.0";

        public static bool Connected = false;
        private static ManualLogSource _logger;

        public string ip;
        public int port;
        public string username;

        private void Awake()
        {
            // Create logger
            _logger = BepInEx.Logging.Logger.CreateLogSource("ULTRANET");

            // Disable Cybergrind Submission
            UMM.UKAPI.DisableCyberGrindSubmission("ULTRANET BLOCKED CYBERGRIND SUBMISSION");
            _logger.LogInfo("ULTRANET Client Initialized.");

            // Scenes
            SceneManager.sceneLoaded += OnSceneLoaded;

            name = pluginName;
        }

        private void OnGUI()
        {
            GUIStyle boxStyle = GUI.skin.box;
            GUIStyle labelStyle = GUI.skin.label;
            GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
            GUIStyle buttonStyle = GUI.skin.button;

            textFieldStyle.fixedHeight = 25;
            textFieldStyle.fontSize = 14;

            if (UMM.UKAPI.GetUKLevelType(SceneHelper.CurrentScene) != UKAPI.UKLevelType.MainMenu)
            {
                // The user isn't in the menu so we don't need to show the UI
                return;
            }

            // If the user isn't connected to a server and in the home menu, show the connection UI
            GUILayout.BeginArea(new Rect(10, 10, 350, 180), boxStyle);
            GUILayout.Label("ULTRANET", labelStyle);

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("IP:", labelStyle);
            ip = GUILayout.TextField(ip, textFieldStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Port:", labelStyle);
            port = int.Parse(GUILayout.TextField(port.ToString(), textFieldStyle));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:", labelStyle);
            username = GUILayout.TextField(username, textFieldStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Connect", buttonStyle))
            {
                Thread connectionThread = new Thread(() => NetworkHandler.TryConnect(ip, (ushort)port, username));
                connectionThread.Start();
            }

            GUILayout.EndArea();
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            // Get scene name
            string sceneName = SceneHelper.CurrentScene;

            // Send Message
            _logger.LogInfo("Scene Loaded: " + sceneName);

            if (!Connected)
                return;

            // Send Packet
            Player local = ClientHandler.LocalPlayer;
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.CHANGE_ROOM, 0, PacketFlag.None,
                sceneName);

            byte[] connectProtocol = packet.ToByteArray();
            PacketHandler.SendPacket(ClientHandler.Channel, connectProtocol);
        }
    }
}