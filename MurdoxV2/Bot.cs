using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.SlashCommands;
using MurdoxV2.Common.Services;

namespace MurdoxV2
{
    public class Bot
    {
        private DiscordClient? Client { get; set; }
        public CommandsNextExtension? Commands { get; set; }
        public SlashCommandsExtension? SlashCommands { get; set; }
        public static InteractivityExtension? Interactivity { get; set; }
        public static Bot Instance { get; } = new Bot();

        public Bot()
        {

        }

        #region RUN

        public async Task RunAsync()
        {
            var dataService = new DataService();
            var token = dataService.GetBotToken();

            var clientConfig = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                AlwaysCacheMembers = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,

            };
        }
        #endregion
    }
}
