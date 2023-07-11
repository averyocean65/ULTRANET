using System;
using System.Threading;
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
        
        private void Awake()
        {
            // Create logger
            _logger = BepInEx.Logging.Logger.CreateLogSource("ULTRANET");

            // Disable Cybergrind Submission
            UMM.UKAPI.DisableCyberGrindSubmission("ULTRANET BLOCKED CYBERGRIND SUBMISSION");
            _logger.LogInfo("ULTRANET Client Initialized.");
        }
        
        private void OnGUI()
        {
            if (UMM.UKAPI.GetUKLevelType(SceneHelper.CurrentScene) != UKAPI.UKLevelType.MainMenu)
            {
                // Show error
                GUI.Label(new Rect(20, 40, 100, 20), "Please connect to a ULTRANET server in the Main Menu.");
                return;
            }
        }
    }
}