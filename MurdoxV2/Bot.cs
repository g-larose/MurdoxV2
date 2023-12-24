using System.Drawing;
using System.Globalization;
using System.Reflection;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using MurdoxV2.Common.Interfaces;
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
            var prefixes = dataService.GetApplicationPrefix();

            var clientConfig = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                AlwaysCacheMembers = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,

            };

            Client = new DiscordClient(clientConfig);
            Client.SessionCreated += OnSessionCreated;

            var iteractivityConfig = new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1),
                PollBehaviour = PollBehaviour.KeepEmojis,
                PaginationEmojis = new PaginationEmojis(),
                PaginationBehaviour = PaginationBehaviour.WrapAround,
                PaginationDeletion = PaginationDeletion.KeepEmojis
            };

            Client.UseInteractivity(iteractivityConfig);

            //configure the services
            var services = new ServiceCollection()
                .AddSingleton<IDataService, DataService>()
                .BuildServiceProvider();

            //set up commands configuration
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { prefixes![0], prefixes![1] },
                EnableDms = true,
                EnableMentionPrefix = true,
                Services = services
            };

            //register commands and slash commands
            this.Commands = this.Client.UseCommandsNext(commandsConfig);
            this.SlashCommands = this.Client.UseSlashCommands();
            RegisterCommands();
            RegisterSlashCommands();

            //connect client to gateway
            await Client.ConnectAsync(new DiscordActivity("Everyone", ActivityType.Watching)).ConfigureAwait(false);

            //wait - this keeps the console running
            await Task.Delay(-1).ConfigureAwait(false);

        }
         #endregion

        private async Task OnSessionCreated(DiscordClient sender, SessionReadyEventArgs args)
        {
            var bot = sender.CurrentUser.Username;
            var todaysDate = DateTime.Now.ToLongDateString();
            Console.ForegroundColor = FromHex("0570FC");
            Console.WriteLine($"\r\n{bot} connected successfully!");
            Console.ForegroundColor = FromHex("D7FC05");
            Console.WriteLine($"Joined: {todaysDate}");

            await SendLogMessage($"{bot} has joined the server.");
        }

        private void RegisterCommands() => Client.GetCommandsNext().RegisterCommands(Assembly.GetExecutingAssembly());
        private void RegisterSlashCommands() => Client.GetSlashCommands().RegisterCommands(Assembly.GetExecutingAssembly());

        #region SEND LOG MESSAGE
        private async Task SendLogMessage(string message) 
        {
            //general channel ID 764184337620140065
            //Suncoast Software log channel ID  888659027607814214
            var logIds = new ulong[] { 888659367824601160, 888659027607814214 }; // add channel id's here for every channel to send logs to.

            foreach (var id in logIds)
            {
                var logChannel = await Client!.GetChannelAsync(id).ConfigureAwait(false);
                await logChannel.SendMessageAsync(message);
            }

        }

        #endregion

        public static ConsoleColor FromHex(string hex)
        {
            int argb = Int32.Parse(hex.Replace("#", ""), NumberStyles.HexNumber);
            Color c = Color.FromArgb(argb);

            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
            index |= (c.R > 64) ? 4 : 0; // Red bit
            index |= (c.G > 64) ? 2 : 0; // Green bit
            index |= (c.B > 64) ? 1 : 0; // Blue bit

            return (System.ConsoleColor)index;
        }
       
    }
}
