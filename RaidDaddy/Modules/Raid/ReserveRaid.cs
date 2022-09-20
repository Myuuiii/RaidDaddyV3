using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Raid;

public class ReserveRaid: ApplicationCommandModule
{
    public ReserveRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("reserve", "Leave the fireteam")]
    public async Task LeaveRaidCommand(InteractionContext context, [Option("target", "The user to reserve a spot for in the fireteam")] DiscordUser user)
    {
        await context.CreateResponseAsync($"Reserving a spot for {user.Username}");
    }
}