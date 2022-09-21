using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;

namespace RaidDaddy.Modules.Raid;

public class SetTimeRaid : ApplicationCommandModule
{
    public SetTimeRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("settime", "Set the elapsed time")]
    public async Task JoinRaidCommand(InteractionContext context, 
        [Option("hours", "Hours elapsed")] long hours, 
        [Option("minutes", "Minutes elapsed")] long minutes, 
        [Option("seconds", "Seconds elapsed")] long seconds)
    {
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is null)
        {
            await context.CreateResponseAsync(content: "You are not in a raid", true);
        }
        else
        {
            RaidFireteam fireteam = await _fireteamRepo.Get(raider.CurrentTeam.Id);
            fireteam.Time = new TimeSpan((int)hours, (int)minutes, (int)seconds);
            await _fireteamRepo.Update(fireteam);
            await context.CreateResponseAsync(content: $"{raider.Mention} set the duration to {fireteam.Time}", ephemeral: false);
        }
    }
}