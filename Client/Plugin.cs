#define USE_GS_AUTH_API

using BepInEx;
using BepInEx.Logging;
using Steamworks;

namespace ULTRANET
{
    public static class PluginInfo
    {
        public const string GUID = "com.ultranet.client";
        public const string NAME = "ULTRANET";
        public const string VERSION = "1.0.0";

        public const int APP_ID = 1229490;

        public static readonly AppId GAME_ID = (AppId)1229490;
    }

    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigUI _config;
        public static bool Connected => SteamClient.IsValid && SteamNetworking.IsP2PPacketAvailable();
        public new static ManualLogSource Logger => BepInEx.Logging.Logger.CreateLogSource(PluginInfo.NAME);

        private void Start()
        {
            SteamClient.Init(PluginInfo.APP_ID); // ULTRAKILL App Id
            if (!SteamClient.IsValid)
            {
                Logger.LogError("Steam is not running!");
                return;
            }

            if (!SteamClient.IsLoggedOn)
            {
                Logger.LogError("You are not logged in!");
                return;
            }

            Logger.LogInfo("ULTRANET Client loaded!");

            // Steam callbacks
            SteamMatchmaking.OnLobbyCreated += NetworkManager.OnLobbyCreated;
            SteamMatchmaking.OnLobbyEntered += NetworkManager.OnLobbyEntered;
            SteamMatchmaking.OnLobbyMemberJoined += NetworkManager.OnLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave += NetworkManager.OnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyInvite += NetworkManager.OnLobbyInvite;
            SteamMatchmaking.OnLobbyGameCreated += NetworkManager.OnLobbyGameCreated;
            SteamFriends.OnGameLobbyJoinRequested += NetworkManager.OnGameLobbyJoinRequested;

            // Config
            _config = new ConfigUI();
            _config.Show();
        }

        private void OnDestroy()
        {
            // Unregister each event
            SteamMatchmaking.OnLobbyCreated -= NetworkManager.OnLobbyCreated;
            SteamMatchmaking.OnLobbyEntered -= NetworkManager.OnLobbyEntered;
            SteamMatchmaking.OnLobbyMemberJoined -= NetworkManager.OnLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave -= NetworkManager.OnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyInvite -= NetworkManager.OnLobbyInvite;
            SteamMatchmaking.OnLobbyGameCreated -= NetworkManager.OnLobbyGameCreated;
            SteamFriends.OnGameLobbyJoinRequested -= NetworkManager.OnGameLobbyJoinRequested;
        }
    }
}