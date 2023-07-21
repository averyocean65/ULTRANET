using PluginConfig.API;
using PluginConfig.API.Decorators;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;
using Steamworks;
using UnityEngine;

namespace ULTRANET
{
    public class ConfigUI
    {
        private static PluginConfigurator _config;
        public static PluginConfigurator Config => _config;

        private void OnEnable()
        {
            Show();
        }

        private void PlayerSettings()
        {
            // Player Settings
            ConfigHeader playerHeader = new ConfigHeader(_config.rootPanel, "Player Settings");

            StringField nickname = new StringField(_config.rootPanel, "Nickname",
                "com.ultranet.player.nickname", SteamClient.Name);

            ColorField nicknameColor = new ColorField(_config.rootPanel, "Nickname Color",
                "com.ultranet.nickname.color",
                Color.white);
        }

        public void Show()
        {
            _config = PluginConfigurator.Create(PluginInfo.NAME, PluginInfo.GUID);

            PlayerSettings();
            HostSettings();
        }

        private void HostSettings()
        {
            ConfigPanel hostPanel = new ConfigPanel(_config.rootPanel, "Host Settings", "com.ultranet.host");

            // Host Settings
            IntField maxPlayers = new IntField(hostPanel, "Max Players", "com.ultranet.host.maxplayers", 8);
            EnumField<LobbyType> publicLobby = new EnumField<LobbyType>(hostPanel, "Lobby Type",
                "com.ultranet.host.lobbytype", LobbyType.Public);
            BoolField allowPvP = new BoolField(hostPanel, "Allow PvP", "com.ultranet.host.pvp", true);

            // Host Settings - Map
            ConfigHeader mapHeader = new ConfigHeader(hostPanel, "Map");
            StringField mapName = new StringField(hostPanel, "Map Name", "com.ultranet.host.map.name", "Level 1-1");

            ButtonField hostButton = new ButtonField(hostPanel, "Host", "com.ultranet.host.button");
            hostButton.onClick += () =>
            {
                NetworkManager.StartHost(maxPlayers.value, publicLobby.value, allowPvP.value, mapName.value);
            };
        }
    }
}