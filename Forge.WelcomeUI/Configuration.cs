using Rocket.API;

namespace Forge.WelcomeUI
{
    public class Configuration : IRocketPluginConfiguration
    {
        public ushort EffectID { get; set; }
        public short EffectKey { get; set; }
        public string DiscordDesc { get; set; }
        public string DiscordURL { get; set; }
        public string WebDesc { get; set; }
        public string WebURL { get; set; }

        public void LoadDefaults()
        {
            EffectID = 12345;
            EffectKey = 1;
            DiscordDesc = "Join our Discord!";
            DiscordURL = "https://discord.gg/example";
            WebDesc = "Visit our website!";
            WebURL = "https://example.com";
        }
    }
}
