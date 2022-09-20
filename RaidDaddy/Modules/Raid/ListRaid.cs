using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Raid;

public class ListRaid: ApplicationCommandModule
{
    public ListRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("list", "List the fireteam details")]
    public async Task LeaveRaidCommand(InteractionContext context)
    {
        await context.CreateResponseAsync("Listing Details");
    }
}