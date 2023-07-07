using BepInEx;
using UnityEngine;

namespace ULTRANET.Client
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string pluginGuid = "com.ultranet.client";
        public const string pluginName = "ULTRANET Client";
        public const string pluginVersion = "0.1.0";

        private void Awake()
        {
            Logger.LogInfo("ULTRANET Initialized.");
        }

        private void OnGUI()
        {
            // UI
            GUI.Label(new Rect(0, 0, 100, 100), "<b>ULTRANET</b>");

            if (GUI.Button(new Rect(0, 50, 100, 50), "CONNECT"))
            {
                Logger.LogError("TODO: Networking");
            }
        }
    }
}