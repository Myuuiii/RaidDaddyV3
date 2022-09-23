using DSharpPlus.SlashCommands;
using Microsoft.VisualBasic;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Extensions.ToEmbed;

namespace RaidDaddy.Modules.Raid;

public class StartCommand : ApplicationCommandModule
{
    public StartCommand(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("Start", "Ping all the members of the fireteam you are in")]
    public async Task StartRaid(InteractionContext context)
    {
        if ((await _raiderRepo.Get(context.User.Id)).CurrentTeam == null)
        {
            await context.CreateResponseAsync(content: "You are not in a fireteam", ephemeral: true);
            return;
        }
        RaidFireteam fireteam = await _fireteamRepo.Get((await _raiderRepo.Get(context.User.Id)).CurrentTeam!.Id);

        string[] mentions = fireteam!.Raiders.Select(x => x.Mention).ToArray();
        await context.CreateResponseAsync(content: $"Transmat firing! {string.Join(' ', mentions)}", embed: fireteam.ToEmbed(), ephemeral: false);
    }
}