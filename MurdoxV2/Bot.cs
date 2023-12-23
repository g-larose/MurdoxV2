using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.SlashCommands;

namespace MurdoxV2
{
    public class Bot
    {
        DiscordClient? client { get; set; }
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

        }
        #endregion
    }
}
