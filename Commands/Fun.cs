using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System.IO;

namespace Suzuna_Chan_2.Commands
{
    public class Fun
    {

        // Define the character class
        public class Character
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }
        }
            private static DateTime LastPlayed = DateTime.MinValue;

            [Command("play")]
            [Description("Play the minigame.")]
            public async Task Play(CommandContext ctx)
            {
                // Check if enough time has passed since last played
                var now = DateTime.Now;
                if ((now - LastPlayed).TotalSeconds < 180)
                {
                    var timeRemaining = (int)Math.Ceiling(180 - (now - LastPlayed).TotalSeconds);
                    await ctx.RespondAsync($"Sorry, you must wait {timeRemaining} seconds before playing again.");
                    return;
                }
            var json = string.Empty;

            using (var fs = File.OpenRead("Characters.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            //This is now working. This is used to make sure the program can read the json file
            var CharacterJson = JsonConvert.DeserializeObject<CharacterJson>(json);

            // Select a random character
            var random = new Random();
            var character = characters[random.Next(characters.Count)];

                // Create the embed to display the character's information
                var embed = new DiscordEmbedBuilder()
                    .WithTitle(character.Name)
                    .WithImageUrl(character.ImageUrl);

                await ctx.RespondAsync(embed: embed);

                // Update the time the minigame was last played
                LastPlayed = now;
            }
        }


    }
}
