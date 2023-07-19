using System.Threading;
using BepInEx.Logging;
using LiteNetLib;
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

        private static ManualLogSource _logger;

        public static NetManager Client;

        public static string IP;
        public static int Port;
        public static string Username;

        private NewMovement _newMovement;

        public static bool Connected
        {
            get
            {
                if (Client != null)
                    return Client.IsRunning;
                return false;
            }
        }

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

        private void Update()
        {
            if (!_newMovement || !Connected)
                return;
        }

        private void OnGUI()
        {
            GUIStyle boxStyle = GUI.skin.box;
            GUIStyle labelStyle = GUI.skin.label;
            GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
            GUIStyle buttonStyle = GUI.skin.button;

            textFieldStyle.fixedHeight = 25;
            textFieldStyle.fontSize = 14;

            if (Connected)
            {
                ConnectedUI();
                return;
            }

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
            IP = GUILayout.TextField(IP, textFieldStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Port:", labelStyle);
            Port = int.Parse(GUILayout.TextField(Port.ToString(), textFieldStyle));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:", labelStyle);
            Username = GUILayout.TextField(Username, textFieldStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Connect", buttonStyle))
            {
                OnConnectPress();
            }

            GUILayout.EndArea();
        }

        void ConnectedUI()
        {
            GUIStyle boxStyle = GUI.skin.box;
            GUIStyle labelStyle = GUI.skin.label;
            GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
            GUIStyle buttonStyle = GUI.skin.button;

            textFieldStyle.fixedHeight = 25;
            textFieldStyle.fontSize = 14;

            GUILayout.BeginArea(new Rect(10, 10, 350, 180), boxStyle);
            GUILayout.Label("ULTRANET", labelStyle);

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Connected as:", labelStyle);
            GUILayout.Label(Username, textFieldStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            if (GUILayout.Button("Disconnect", buttonStyle))
            {
                OnDisconnectPress();
            }

            GUILayout.EndArea();
        }

        private void OnDisconnectPress()
        {
            Client.Stop();
            HudMessageReceiver.Instance.SendHudMessage("Disconnected from server.");
        }

        private void OnConnectPress()
        {
            new Thread(() =>
            {
                var tryConnect = Networking.Client.TryConnect(IP, (ushort)Port, Username);
                tryConnect.Start();
            }).Start();
            HudMessageReceiver.Instance.SendHudMessage($"Attempting connection to {IP}:{Port}...");
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            // Get scene name
            string sceneName = SceneHelper.CurrentScene;

            // Send Message
            _logger.LogInfo("Scene Loaded: " + sceneName);

            // Get New Movement
            _newMovement = FindObjectOfType<NewMovement>();

            if (!Connected)
                return;

            // TODO: Get players in room
        }
    }
}