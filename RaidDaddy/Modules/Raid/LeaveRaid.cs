using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;

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
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is null)
        {
            await context.CreateResponseAsync(content: "You are not in a raid", true);
        }
        else
        {
            RaidFireteam fireteam = await _fireteamRepo.Get(raider.CurrentTeam.Id);
            fireteam.Raiders.Remove(raider);
            await _fireteamRepo.Update(fireteam);
            await context.CreateResponseAsync(content: $"{raider.Mention} has left the raid", ephemeral: false);
        }
    }
}
