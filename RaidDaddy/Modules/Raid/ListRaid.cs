using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Extensions.ToEmbed;

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
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is null)
        {
            await context.CreateResponseAsync(content: "You are not in a raid", true);
        }
        else
        {
            RaidFireteam fireteam = await _fireteamRepo.Get(raider.CurrentTeam.Id);
            await context.CreateResponseAsync(fireteam.ToEmbed(), false);
        }
    }
}