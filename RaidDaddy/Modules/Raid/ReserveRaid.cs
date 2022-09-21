using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;

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
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is null)
        {
            await context.CreateResponseAsync(content: "You are not in a raid", true);
        }
        else
        {
            RaidFireteam fireteam = await _fireteamRepo.Get(raider.CurrentTeam.Id);
            Raider targetRaider = await _raiderRepo.Get(user.Id);

            if (targetRaider.CurrentTeam is not null)
            {
                await context.CreateResponseAsync("That user is already in a raid", true);
                return;
            }
            
            if (fireteam.Raiders.Contains(targetRaider))
            {
                await context.CreateResponseAsync("That user is already in the raid", true);
                return;
            }
            
            fireteam.Raiders.Add(targetRaider);
            
            await _fireteamRepo.Update(fireteam);
            await context.CreateResponseAsync(content: $"{targetRaider.Mention} has been added to the raid by {raider.Mention}", ephemeral: false);
        }
    }
}