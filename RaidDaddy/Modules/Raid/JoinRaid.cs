using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Raid;

public class JoinRaid : ApplicationCommandModule
{
    public JoinRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("join", "Join the currently active raid")]
    public async Task JoinRaidCommand(InteractionContext context)
    {
        await context.CreateResponseAsync("Joining");
    }
}