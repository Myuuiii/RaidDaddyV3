using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Role;

public class SetRaiderRole: ApplicationCommandModule
{
    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;
    private readonly RaiderRoleRepository _raiderRole;
    
    public SetRaiderRole(FireteamRepository fireteamRepo, RaiderRepository raiderRepo, RaiderRoleRepository raiderRole)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
        _raiderRole = raiderRole;
    }
    
    [SlashCommand("raiderrole", "Set the raider role")]
    public async Task RaiderRole(InteractionContext ctx, [Option("role", "The role to set")] DiscordRole role)
    {
        _raiderRole.Set(role);
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Raider role set to '{role.Name}'"));
    }
}