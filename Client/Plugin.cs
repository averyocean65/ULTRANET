using System.Threading;
using BepInEx.Logging;
using Google.Protobuf;
using ULTRANET.Client.Networking;
using ULTRANET.Core;
using ULTRANET.Core.Protobuf;
using UMM;
using UnityEngine;
using UnityEngine.SceneManagement;
using Transform = ULTRANET.Core.Protobuf.Transform;

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

        private UnityEngine.Vector3 _lastPosition;
        private Quaternion _lastRotation;
        private float _lastTime;

        private NewMovement _newMovement;

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

            UnityEngine.Vector3 position = _newMovement.transform.position;
            Quaternion rotation = _newMovement.transform.rotation;
            UnityEngine.Vector3 scale = _newMovement.transform.localScale;

            if (position == _lastPosition &&
                rotation == _lastRotation)
                return;

            // Send a network event
            Player local = ClientHandler.LocalPlayer;

            if (local == null)
                return;

            Transform transformMessage = TransformUtils.ToTransform(position, rotation.eulerAngles, scale);

            // Stupid hack because Protobuf doesn't like me
            string transformString = $"{transformMessage.PosX};{transformMessage.PosY};{transformMessage.PosZ};" +
                                     $"{transformMessage.RotX};{transformMessage.RotY};{transformMessage.RotZ};" +
                                     $"{transformMessage.SclX};{transformMessage.SclY};{transformMessage.SclZ}";

            _logger.LogInfo(transformString);

            // Send packet
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.PLAYER_TRANSFORM_UPDATE, local.Id,
                PacketFlag.Player, transformString);

            PacketHandler.SendPacket(ClientHandler.Channel, packet.ToByteArray());
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
            GUILayout.Label(username, textFieldStyle);
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
            NetworkHandler.TryDisconnect(ClientHandler.Channel);
        }

        private void OnConnectPress()
        {
            ClientHandler.Logger = _logger;

            new Thread(() => NetworkHandler.TryConnect(ip, (ushort)port, username)).Start();
            HudMessageReceiver.Instance.SendHudMessage($"Attempting connection to {ip}:{port}...");
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

            // Send Packet
            Player local = ClientHandler.LocalPlayer;
            DynamicPacket packet = PacketHandler.GeneratePacket(ProtocolHeaders.CHANGE_ROOM, 0, PacketFlag.Player,
                sceneName);

            byte[] connectProtocol = packet.ToByteArray();
            PacketHandler.SendPacket(ClientHandler.Channel, connectProtocol);
        }
    }
}