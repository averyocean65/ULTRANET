using System.Collections.Generic;
using Steamworks;
using Steamworks.Data;
using UnityEngine.SceneManagement;

namespace ULTRANET
{
    public enum LobbyType
    {
        Public,
        FriendsOnly,
        Private
    }

    public class NetworkManager
    {
        private static List<SteamId> _currentLobbyMembers = new List<SteamId>();
        public static Lobby CurrentLobby { get; private set; }
        public static LobbyInfo CurrentLobbyInfo { get; private set; }

        public static bool IsHost
        {
            get { return CurrentLobby.Owner.Id == SteamClient.SteamId; }
        }

        public static bool PreConnectChecks(string username)
        {
            // Check if the player is already connected
            if (Plugin.Connected)
            {
                Plugin.Logger.LogWarning("You are already connected to a server!");
                HudMessageReceiver.Instance.SendHudMessage(
                    "<color=red>You are already connected to a server!</color>"
                );
                return false;
            }

            // Check if the nickname is empty
            if (string.IsNullOrEmpty(username))
            {
                Plugin.Logger.LogWarning("Nickname cannot be empty!");
                HudMessageReceiver.Instance.SendHudMessage(
                    "<color=red>Nickname cannot be empty!</color>"
                );
                return false;
            }

            // Check if the nickname is too long
            if (username.Length > 16)
            {
                Plugin.Logger.LogWarning("Nickname cannot be longer than 16 characters!");
                HudMessageReceiver.Instance.SendHudMessage(
                    "<color=red>Nickname cannot be longer than 16 characters!</color>"
                );
                return false;
            }

            return true;
        }

        public static void OnLobbyCreated(Result result, Lobby lobby)
        {
            if (result != Result.OK)
            {
                Plugin.Logger.LogError("Failed to create a lobby!");
                return;
            }

            Plugin.Logger.LogInfo("Successfully created a lobby!");
            HudMessageReceiver.Instance.SendHudMessage(
                "<color=yellow>Successfully created a lobby!</color>"
            );

            CurrentLobby = lobby;
            CurrentLobbyInfo = LobbyInfo.Create(lobby);
        }

        public static void OnLobbyEntered(Lobby lobby)
        {
            // if (IsHost)
            // {
            //     Plugin.Logger.LogInfo("Successfully join your own lobby lobby!");
            //     return;
            // }

            LobbyInfo info = LobbyInfo.Create(lobby);

            // Compare version
            if (info.Version != PluginInfo.VERSION)
            {
                Plugin.Logger.LogError("Version mismatch!");
                HudMessageReceiver.Instance.SendHudMessage(
                    "<color=red>Couldn't join lobby due to version mismatch!</color>"
                );

                lobby.Leave();
                return;
            }

            Plugin.Logger.LogInfo("Successfully joined the lobby!");
            CurrentLobby = lobby;
            CurrentLobbyInfo = info;

            // Join Lobby Map
            SceneHelper.LoadScene(CurrentLobbyInfo.Map);
        }

        public static void OnLobbyMemberJoined(Lobby arg1, Friend arg2)
        {
        }

        public static void OnLobbyMemberLeave(Lobby arg1, Friend arg2)
        {
        }

        // Friend sent you an invite
        public static void OnLobbyInvite(Friend friend, Lobby lobby)
        {
            Plugin.Logger.LogInfo($"Invite from {friend.Name}");
            HudMessageReceiver.Instance.SendHudMessage(
                $"<color=yellow>{friend.Name} invited you to a lobby!</color>"
            );
        }

        public static void OnLobbyGameCreated(Lobby lobby, uint id, ushort port, SteamId steamId)
        {
            Plugin.Logger.LogInfo("Lobby game created!");
        }

        // When you accept an invite or join a lobby from the Steam UI
        public static async void OnGameLobbyJoinRequested(Lobby lobby, SteamId arg2)
        {
            RoomEnter result = await lobby.Join();
            if (result != RoomEnter.Success)
            {
                Plugin.Logger.LogError("Failed to join the lobby!");
                return;
            }

            Plugin.Logger.LogInfo("Successfully joined the lobby!");
            CurrentLobby = lobby;
        }

        public static void OnP2PSessionRequest(SteamId arg1)
        {
            if (CurrentLobby.MemberCount >= CurrentLobby.MaxMembers)
                return;

            _currentLobbyMembers.Add(arg1);
            SteamNetworking.AcceptP2PSessionWithUser(arg1);
        }

        public static async void StartHost(int maxPlayers, LobbyType type, bool allowPvP, string map)
        {
            SteamNetworking.AllowP2PPacketRelay(true);
            Lobby? lobby = await SteamMatchmaking.CreateLobbyAsync(maxPlayers);

            if (!lobby.HasValue)
            {
                Plugin.Logger.LogError("Failed to create a lobby!");
                HudMessageReceiver.Instance.SendHudMessage(
                    "<color=red>Failed to create a lobby!</color>"
                );
                return;
            }

            Plugin.Logger.LogInfo("Creating Lobby...");

            Lobby temp = lobby.Value;

            temp.MaxMembers = maxPlayers;
            temp.SetFriendsOnly();
            temp.SetData("com.ultranet.lobby.version", PluginInfo.VERSION);
            temp.SetData("com.ultranet.lobby.allowPvP", allowPvP.ToString());
            temp.SetData("com.ultranet.lobby.map", map);

            switch (type)
            {
                case LobbyType.Public:
                    temp.SetPublic();
                    break;
                case LobbyType.FriendsOnly:
                    temp.SetFriendsOnly();
                    break;

                default:
                case LobbyType.Private:
                    temp.SetPrivate();
                    break;
            }

            temp.SetJoinable(true);
            CurrentLobby = temp;
        }

        public static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (!IsHost || SceneHelper.CurrentScene == "Main Menu")
                return;

            CurrentLobby.SetData("com.ultranet.lobby.map", SceneHelper.CurrentScene);
            CurrentLobbyInfo.Map = SceneHelper.CurrentScene;
        }
    }
}