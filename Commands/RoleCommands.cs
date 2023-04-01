using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Suzuna_Chan_2.Commands
{
    public class RoleCommands : BaseCommandModule
    {
        [Command("assign")]
        [Description("Allows users to self-assign roles.")]
        public async Task assign(CommandContext ctx, [RemainingText] string roleName)
        {
            if (roleName == null)
            {
                await ctx.RespondAsync("You need to type a role in. Check <#503960758808215553> for a list of roles");
                return;
            }
            if (ctx.Guild == null)
            {
                await ctx.RespondAsync("I'm not currently connected to a guild.");
                return;
            }

            // Define the list of allowed roles
            var allowedRoles = new List<DiscordRole>
            {
                ctx.Guild.GetRole(729353889987690559), //Daily
                ctx.Guild.GetRole(729362226493784084), //Vtuber
                ctx.Guild.GetRole(753158894641217619), //MHW
                ctx.Guild.GetRole(945977842032869427), //Serialline
                ctx.Guild.GetRole(945978663109804082), //GreLine
                ctx.Guild.GetRole(531610323057246211), //MHW
                ctx.Guild.GetRole(622003059387662346), //VN
                ctx.Guild.GetRole(531610323057246211), //Bepclub
                ctx.Guild.GetRole(649602264389910538), //No Nonaka Club
                ctx.Guild.GetRole(649286822714933250), //Phanline Waiting Room
                ctx.Guild.GetRole(648095467959484416), //FF Waiting Room
                ctx.Guild.GetRole(589959839703105604), //1-3 Club
                ctx.Guild.GetRole(590769025924071437), //Grecale
                ctx.Guild.GetRole(569079600848568321), //Kuso
                ctx.Guild.GetRole(503972209795268609), //I Wish I was a Neet Again
                ctx.Guild.GetRole(608763705240322090), //Yamalaze
                ctx.Guild.GetRole(539219123964608522), //Phanstream
                ctx.Guild.GetRole(630202516654784532), //Banned
            };

            try
            {
                // Find the role with the specified name
                var role = allowedRoles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

                if (role == null)
                {
                    await ctx.RespondAsync($"Sorry, the role '{roleName}' is not available for self-assigning.");
                    return;
                }

                if (ctx.Member.Roles.Contains(role))
                {
                    await ctx.Member.RevokeRoleAsync(role);
                    await ctx.RespondAsync($"The role '{role.Name}' is removed.");
                }
                else
                {

                    await ctx.Member.GrantRoleAsync(role);
                    await ctx.RespondAsync($"You now have the role '{role.Name}'.");
                }
            }

            catch (Exception ex)
            {
                await ctx.RespondAsync($"Error: {ex.Message}");
            }
        }
    }
}