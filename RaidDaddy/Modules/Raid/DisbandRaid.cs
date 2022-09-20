using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Raid;

public class DisbandRaid : ApplicationCommandModule
{
    public DisbandRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("disband", "Disbands the raid (Remove all members and archive)")]
    public async Task DisbandRaidCommand(InteractionContext context)
    {
        await context.CreateResponseAsync("Disbanding");
    }
}