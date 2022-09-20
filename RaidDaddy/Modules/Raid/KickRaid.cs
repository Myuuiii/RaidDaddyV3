using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;

namespace RaidDaddy.Modules.Raid;

public class KickRaid: ApplicationCommandModule
{
    public KickRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("kick", "Kick a user from the fireteam (L + ratio)")]
    public async Task JoinRaidCommand(InteractionContext context)
    {
        await context.CreateResponseAsync("Joining");
    }
}