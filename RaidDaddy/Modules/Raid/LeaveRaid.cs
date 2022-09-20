using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Raid;

public class LeaveRaid : ApplicationCommandModule
{
    public LeaveRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("leave", "Leave the fireteam")]
    public async Task LeaveRaidCommand(InteractionContext context)
    {
        await context.CreateResponseAsync("Leaving");
    }
}
