using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Enums;

namespace RaidDaddy.Modules.Raid;

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
        await context.CreateResponseAsync(content: $"Your subclass has been changed to {subclass}", ephemeral: true);
    }
    
    [SlashCommand("class", "Set your class")]
    public async Task DisbandRaidCommand(InteractionContext context, [Option("class", "Your subclass")] Destiny2Class d2Class)
    {
        Raider raider = await _raiderRepo.Get(context.User.Id);
        raider.Class = d2Class ;
        await _raiderRepo.Update(raider);
        await context.CreateResponseAsync(content: $"Your class has been changed to {d2Class}", ephemeral: true);
    }
}