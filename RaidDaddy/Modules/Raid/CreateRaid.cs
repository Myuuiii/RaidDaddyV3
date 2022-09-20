using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Extensions;

namespace RaidDaddy.Modules.Raid;

public class CreateRaid : ApplicationCommandModule
{
    public CreateRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;
    
    [SlashCommand("create", "Create a new fireteam")]
    public async Task CreateRaidCommand(InteractionContext context,
        [Option("raid", "The raid you want to create")] Raid raid)
    {
        await context.CreateResponseAsync($"Creating raid {raid.ToRaidString()}");
    }

    public enum Raid
    {
        [ChoiceName("Garden of Salvation")]
        GOS,
        [ChoiceName("Deep Stone Crypt")]
        DSC,
        [ChoiceName("Last Wish")]
        LW,
        [ChoiceName("King's Fall")]
        KF,
        [ChoiceName("Vow of the Disciple")]
        VOTD,
        [ChoiceName("Vault of Glass")]
        VOG,
    }
}