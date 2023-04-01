using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suzuna_Chan_2.commands
{

    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("How far away am I from the Discord servers")]
        public async Task Ping(CommandContext ctx)
        {
            var timer = DateTime.Now.Millisecond - ctx.Message.CreationTimestamp.Millisecond;
            await ctx.Channel.SendMessageAsync("Pong! " + timer + "ms").ConfigureAwait(false);

        }
        [Command("profile")]
        [Description("Displays the user's profile information.")]
        public async Task Profile(CommandContext ctx, DiscordMember member = null)
        {
            // If member is not provided, use the author of the command
            if (member == null) member = ctx.Member;


            var embed = new DiscordEmbedBuilder()
                .WithTitle(member.DisplayName)
                .WithThumbnail(member.AvatarUrl)
                .WithColor(DiscordColor.DarkBlue)
                .AddField("Username", member.Username, inline: true)
                .AddField("Discriminator", member.Discriminator, inline: true)
                .AddField("ID", member.Id.ToString(), inline: false);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }

}
