using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Extensions.ToEmbed;

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
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is null)
        {
            await context.CreateResponseAsync(content: "You are not in a raid", ephemeral: true);
            return;
        }
        RaidFireteam fireteam = await _fireteamRepo.Get(raider.CurrentTeam.Id);
        fireteam.Raiders.Clear();
        await _fireteamRepo.Update(fireteam);
        await context.CreateResponseAsync(content: "Raid disbanded", ephemeral: false);
    }
}