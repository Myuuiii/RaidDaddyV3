using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;

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
    public async Task JoinRaidCommand(InteractionContext context, [Option("target", "The user to kick from the fireteam")] DiscordUser user)
    {
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is null)
        {
            await context.CreateResponseAsync(content: "You are not in a raid", true);
        }
        else
        {
            Raider targetUser = await _raiderRepo.Get(user.Id);
            RaidFireteam fireteam = await _fireteamRepo.Get(raider.CurrentTeam.Id);
            fireteam.Raiders.Remove(targetUser);
            await _fireteamRepo.Update(fireteam);
            await context.CreateResponseAsync(content: $"{raider.Mention} has been kicked from the raid by {raider.Mention}", ephemeral: false);
        }
    }
}