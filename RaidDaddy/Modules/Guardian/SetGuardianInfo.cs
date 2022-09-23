using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Enums;
using RaidDaddy.Extensions.ToEmbed;

namespace RaidDaddy.Modules.Guardian;

public class SetGuardianInfo : ApplicationCommandModule
{
    public SetGuardianInfo(RaiderRepository raiderRepo)
    {
        _raiderRepo = raiderRepo;
    }

    private readonly RaiderRepository _raiderRepo;

    [SlashCommand("subclass", "Set your subclass")]
    public async Task DisbandRaidCommand(InteractionContext context, [Option("subclass", "Your subclass")] Destiny2Subclass subclass)
    {
        Raider raider = await _raiderRepo.Get(context.User.Id);
        raider.Subclass = subclass;
        await _raiderRepo.Update(raider);
        await context.CreateResponseAsync(embed: subclass.ToEmbed(), ephemeral: true);
    }
    
    [SlashCommand("class", "Set your class")]
    public async Task DisbandRaidCommand(InteractionContext context, [Option("class", "Your subclass")] Destiny2Class d2Class)
    {
        Raider raider = await _raiderRepo.Get(context.User.Id);
        raider.Class = d2Class ;
        await _raiderRepo.Update(raider);
        await context.CreateResponseAsync(embed: d2Class.ToEmbed(), ephemeral: true);
    }
}