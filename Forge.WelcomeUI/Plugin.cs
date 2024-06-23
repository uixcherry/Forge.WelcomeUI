using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace Forge.WelcomeUI
{
    public class Plugin : RocketPlugin<Configuration>
    {
        public static Plugin Instance { get; private set; }

        protected override void Load()
        {
            Instance = this;

            U.Events.OnPlayerConnected += OnPlayerConnected;
            EffectManager.onEffectButtonClicked += OnEffectButtonClicked;
        }

        protected override void Unload()
        {
            Instance = null;

            U.Events.OnPlayerConnected -= OnPlayerConnected;
            EffectManager.onEffectButtonClicked -= OnEffectButtonClicked;
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            EffectOpen(player);
        }

        private void OnEffectButtonClicked(Player player, string buttonName)
        {
            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromPlayer(player);

            switch (buttonName)
            {
                case "forge.welcome_close":
                    EffectManager.askEffectClearByID(Configuration.Instance.EffectID, unturnedPlayer.Player.channel.owner.transportConnection);
                    unturnedPlayer.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    break;
                case "forge.welcome_discord":
                    unturnedPlayer.Player.sendBrowserRequest(Configuration.Instance.DiscordDesc, Configuration.Instance.DiscordURL);
                    break;
                case "forge.welcome_web":
                    unturnedPlayer.Player.sendBrowserRequest(Configuration.Instance.WebDesc, Configuration.Instance.WebURL);
                    break;
            }
        }

        public void EffectOpen(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Configuration.Instance.EffectID, Configuration.Instance.EffectKey, player.Player.channel.owner.transportConnection, true);

            EffectManager.sendUIEffectText(Configuration.Instance.EffectKey, player.Player.channel.owner.transportConnection, true, "forge.header_text", Translate("header_text"));
            EffectManager.sendUIEffectText(Configuration.Instance.EffectKey, player.Player.channel.owner.transportConnection, true, "forge.title_text", Translate("title_text"));
            EffectManager.sendUIEffectText(Configuration.Instance.EffectKey, player.Player.channel.owner.transportConnection, true, "forge.description_text", Translate("description_text"));

            player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);
        }

        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "header_text", "Welcome manager" },
            { "title_text", "Title" },
            { "description_text", "Description" }
        };
    }
}