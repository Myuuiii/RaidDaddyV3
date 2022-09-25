using System.Text;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities.Roles;

namespace RaidDaddy.Modules.Roles;

[SlashCommandGroup("category", "Manage categories")]
public class RoleCategoryManagement: ApplicationCommandModule
{
    private readonly RoleCategoryRepository _roleCategoryRepository;
    private readonly RoleRepository _roleRepository;
    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;
    
    public RoleCategoryManagement(RoleCategoryRepository roleCategoryRepository, RoleRepository roleRepository, FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _roleCategoryRepository = roleCategoryRepository;
        _roleRepository = roleRepository;
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }
    
    [SlashCommand("add", "Add a category")]
    public async Task AddCommand(InteractionContext ctx,
        [Option("name", "The name of the category")] string name,
        [Option("description", "The description of the category")] string description,
        [Option("allowMultiSelect", "Whether or not the category allows multiple roles to be selected")] bool allowMultiSelect)
    {
        if (await _roleCategoryRepository.RoleCategoryWithNameExists(name))
        {
            await ctx.CreateResponseAsync("Category with that name already exists", ephemeral: true);
            return;
        }

        RoleCategory category = new()
        {
            Name = name,
            Description = description,
            AllowMultiSelect = allowMultiSelect
        };

        await _roleCategoryRepository.CreateRoleCategory(category);
        await ctx.CreateResponseAsync("Category created", ephemeral: true);
    }

    [SlashCommand("remove", "Remove a category")]
    public async Task RemoveCommand(InteractionContext ctx, 
        [Option("name", "The name of the category")] string name)
    {
        if (!await _roleCategoryRepository.RoleCategoryWithNameExists(name))
        {
            await ctx.CreateResponseAsync("Category with that name does not exist", ephemeral: true);
            return;
        }

        RoleCategory category = await _roleCategoryRepository.GetRoleCategoryByName(name);
        await _roleCategoryRepository.DeleteRoleCategory(category);
        await ctx.CreateResponseAsync("Category has been deleted", ephemeral: true);
    }

    [SlashCommand("list", "List all the roles in a category")]
    public async Task ListCommand(InteractionContext ctx,
        [Option("name", "The name of the category")]
        string name)
    {
        if (!await _roleCategoryRepository.RoleCategoryWithNameExists(name))
        {
            await ctx.CreateResponseAsync("Category with that name does not exist", ephemeral: true);
            return;
        }
        
        RoleCategory category = await _roleCategoryRepository.GetRoleCategoryByName(name);
        StringBuilder sb = new StringBuilder();
        foreach (RoleCategoryEntry role in category.Entries)
            sb.AppendLine($"- <@&{role.RoleId}>");
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            .WithTitle($"Roles in category: {category.Name}")
            .WithDescription(sb.ToString())
            .WithColor(DiscordColor.Green);
        await ctx.CreateResponseAsync(embed: embed.Build(), ephemeral: true);
    }
}