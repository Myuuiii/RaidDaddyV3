using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Enums;
using RaidDaddy.Extensions;
using RaidDaddy.Extensions.ToEmbed;

namespace RaidDaddy.Modules.Raid;

public class CreateRaid : ApplicationCommandModule
{
    public CreateRaid(FireteamRepository fireteamRepo, RaiderRepository raiderRepo, RaiderRoleRepository raiderRole)
    {
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
        _raiderRole = raiderRole;
    }

    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;
    private readonly RaiderRoleRepository _raiderRole;
    
    [SlashCommand("create", "Create a new fireteam")]
    public async Task CreateRaidCommand(InteractionContext context,
        [Option("raid", "The raid you want to create")] Raid raid)
    {
        Raider creator = await _raiderRepo.Get(context.User.Id);

        if (creator.CurrentTeam is not null)
        {
            await context.CreateResponseAsync("You are already in a fireteam", true);
            return;
        }
        
        RaidFireteam fireteam = new()
        {
            Raid = raid.ToDestiny2Raid(),
            Date = DateTime.Now,
            Encounter = Destiny2Encounter.CLEAN
        };
        fireteam.Raiders.Add(creator);
        await _fireteamRepo.Add(fireteam);
        await context.CreateResponseAsync(content: $"{context.User.Username} created a new raid <@&{_raiderRole.GetId()}>", embed:fireteam.ToEmbed(), false);
    }

    public enum Raid
    {
        [ChoiceName("Garden of Salvation")]
        Gos,
        [ChoiceName("Deep Stone Crypt")]
        Dsc,
        [ChoiceName("Last Wish")]
        Lw,
        [ChoiceName("King's Fall")]
        Kf,
        [ChoiceName("Vow of the Disciple")]
        Votd,
        [ChoiceName("Vault of Glass")]
        Vog,
    }
}