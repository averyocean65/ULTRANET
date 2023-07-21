using Steamworks.Data;

namespace ULTRANET
{
    public class LobbyInfo
    {
        public bool AllowPvP;
        public string Map;
        public int MaxPlayers;
        public string Version;

        public static LobbyInfo Create(Lobby lobby)
        {
            return new LobbyInfo()
            {
                Version = lobby.GetData("com.ultranet.lobby.version"),
                MaxPlayers = lobby.MaxMembers,
                AllowPvP = lobby.GetData("com.ultranet.lobby.allowpvp") == "true",
                Map = lobby.GetData("com.ultranet.lobby.map")
            };
        }
    }
}