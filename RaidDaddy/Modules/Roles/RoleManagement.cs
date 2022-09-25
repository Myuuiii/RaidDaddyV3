using System.Text;
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
    public async Task SendCommand(InteractionContext ctx,
        [Option("name", "The name of the category")] string name,
        [Option("channel", "Channel to send the message in")] DiscordChannel channel)
    {
        RoleCategory category = await _roleCategoryRepository.GetRoleCategoryByName(name);
        List<DiscordSelectComponentOption> componentOptions = (from role in category.Entries let discordRole = ctx.Guild.GetRole(role.RoleId) select new DiscordSelectComponentOption(discordRole.Name, role.RoleId.ToString(), role.Description, false)).ToList();
        DiscordSelectComponent roleSelectComponent = new DiscordSelectComponent($"role-select:{name}", "Select your roles", componentOptions);
        
        DiscordMessageBuilder builder = new DiscordMessageBuilder();
        builder.AddComponents(new DiscordComponent[] {roleSelectComponent});
        builder.AddEmbed(new DiscordEmbedBuilder()
            .WithTitle($"Select your roles - {category.Name}")
            .WithDescription(category.Description));
        await builder.SendAsync(channel);
        await ctx.CreateResponseAsync("Message sent", ephemeral: true);
    }
    
    [SlashCommand("list", "List all roles in a category")]
    public async Task ListCommand(InteractionContext ctx,
        [Option("name", "The name of the category")] string name)
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