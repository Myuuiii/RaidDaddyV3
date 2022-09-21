using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;

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
        Raider raider = await _raiderRepo.Get(context.User.Id);
        if (raider.CurrentTeam is not null)
        {
            await context.CreateResponseAsync(content: "You are already in a raid", true);
        }
        else
        {
            RaidFireteam fireteam = await _fireteamRepo.GetLatest();
            fireteam.Raiders.Add(raider);
            await _fireteamRepo.Update(fireteam);
            await context.CreateResponseAsync(content: $"{raider.Mention} joined the raid", ephemeral: false);
        }
    }

    [SlashCommand("join_id", "Join a raid by ID")]
    public async Task JoinRaidIdCommand(InteractionContext context, [Option("raidId", "Raid Id")] long id)
    {
        await context.CreateResponseAsync(content: $"Not implemented");
    }
}