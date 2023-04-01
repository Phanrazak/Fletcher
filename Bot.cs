using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Suzuna_Chan_2.commands;
using Suzuna_Chan_2.Commands;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suzuna_Chan_2
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public DiscordCustomStatus DiscordCustomStatus { get; private set; }
        public async Task RunAsync()
        {
            var json = string.Empty;

            //Config.json is located in Suzuna-Chan 2/Bin/Debug
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            //This is now working. This is used to make sure the program can read the json file
            var configJson = JsonConvert.DeserializeObject<configjson>(json);

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All
            };
            Client = new DiscordClient(config);

            await Client.ConnectAsync();
            List<string> questionmark = new List<string>();
            questionmark.Add(configJson.Prefix);

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = questionmark,
                EnableMentionPrefix = true,
                EnableDms = true,
            };


            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<BasicCommands>();
            Commands.RegisterCommands<RoleCommands>();


            await Task.Delay(-1);

        }
        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
        public async Task BotMentioned(DiscordClient client, MessageCreateEventArgs e)
        {
            // Check if the message mentions the bot without a command
            if (e.Message.MentionedUsers.Any(u => u == client.CurrentUser) && !e.Message.Content.StartsWith("?"))
            {
                // Send a reply message
                await e.Channel.SendMessageAsync($"Hi {e.Author.Mention}, check ?help");
            }
        }
    }
}
