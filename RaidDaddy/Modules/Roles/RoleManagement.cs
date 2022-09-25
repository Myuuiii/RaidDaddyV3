using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities.Roles;

namespace RaidDaddy.Modules.Roles;

[SlashCommandGroup("role", "Manage roles for this server")]
public class RoleManagement : ApplicationCommandModule
{
    private readonly RoleCategoryRepository _roleCategoryRepository;
    private readonly RoleRepository _roleRepository;
    private readonly FireteamRepository _fireteamRepo;
    private readonly RaiderRepository _raiderRepo;
    
    public RoleManagement(RoleCategoryRepository roleCategoryRepository, RoleRepository roleRepository, FireteamRepository fireteamRepo, RaiderRepository raiderRepo)
    {
        _roleCategoryRepository = roleCategoryRepository;
        _roleRepository = roleRepository;
        _fireteamRepo = fireteamRepo;
        _raiderRepo = raiderRepo;
    }

    [SlashCommand("add", "Add a role to a category")]
    public async Task AddCommand(InteractionContext ctx,
        [Option("categoryName", "The name of the category to add this role to")] string categoryName,
        [Option("role", "The role to add to the given category")] DiscordRole role,
        [Option("description", "The description of this role")] string description)
    {
        if (!await _roleCategoryRepository.RoleCategoryWithNameExists(categoryName))
        {
            await ctx.CreateResponseAsync("A category with that name does not exist", ephemeral: true);
            return;
        }

        if (await _roleRepository.RoleCategoryEntryExists(categoryName, role.Id))
        {
            await ctx.CreateResponseAsync("This role is already in this category");
            return;
        }

        RoleCategoryEntry entry = new()
        {
            RoleId = role.Id,
            Category = await _roleCategoryRepository.GetRoleCategoryByName(categoryName),
            Description = description
        };

        await _roleRepository.CreateRoleCategoryEntry(entry);
        await ctx.CreateResponseAsync("Role has been added to the category", ephemeral: true);
    }

    [SlashCommand("remove", "Remove a role from a category")]
    public async Task RemoveCommand(InteractionContext ctx,
        [Option("categoryName", "The name of the category to remove the role from")] string categoryName,
        [Option("role", "The role you want to remove from the category")] DiscordRole role)
    {
        if (!await _roleCategoryRepository.RoleCategoryWithNameExists(categoryName))
        {
            await ctx.CreateResponseAsync("A category with that name does not exist", ephemeral: true);
            return;
        }

        if (!await _roleRepository.RoleCategoryEntryExists(categoryName, role.Id))
        {
            await ctx.CreateResponseAsync("This role does not exist in the given category");
            return;
        }

        RoleCategoryEntry entry = await _roleRepository.GetRoleCategoryEntry(categoryName, role.Id);
        await _roleRepository.DeleteRoleCategoryEntry(entry);
        await ctx.CreateResponseAsync("Role has been removed from the category", ephemeral: true);
    }
    
   

    [SlashCommand("send", "Send the role message component to the selected channel")]
    public async Task SendCommand(InteractionContext ctx)
    {
    }
    
    [SlashCommand("list", "List all roles and categories")]
    public async Task ListCommand(InteractionContext ctx)
    {
    }
}