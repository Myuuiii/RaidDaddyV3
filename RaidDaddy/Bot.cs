using System.Reflection;
using System.Text;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using RaidDaddy.Data;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Entities;
using RaidDaddy.Modules.Guardian;
using RaidDaddy.Entities.Roles;
using RaidDaddy.Modules.Raid;
using RaidDaddy.Modules.Role;
using RaidDaddy.Modules.Roles;

namespace RaidDaddy;

public sealed class Bot
{
    private ServiceProvider _serviceCollection;
    private DataContext _db;
    private DiscordClient _client;
    private SlashCommandsExtension? _slash;
    
    private FireteamRepository _ftRepo;
    private RaiderRepository _rdRepo;
    private RaiderRoleRepository _rdrRepo;
    private RoleCategoryRepository _rcRepo;
    private RoleRepository _rRepo;
    
    private const ulong _guildId = 887198526579281920;
    private static Task Main(string[] args) => new Bot().MainAsync();

    private async Task MainAsync()
    {
        _db = new DataContext();
        _ftRepo = new FireteamRepository(_db);        
        _rdRepo = new RaiderRepository(_db);
        _rdrRepo = new RaiderRoleRepository();
        
        _rcRepo = new RoleCategoryRepository(_db);
        _rRepo = new RoleRepository(_db);
        _client = new DiscordClient(new DiscordConfiguration()
        {
            Token = Environment.GetEnvironmentVariable("token"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.All,
        });
        
        _serviceCollection = new ServiceCollection()
            .AddSingleton(_ftRepo)
            .AddSingleton(_rdRepo)
            .AddSingleton(_rdrRepo)
            .AddSingleton(_rcRepo)
            .AddSingleton(_rRepo)
            .BuildServiceProvider();

        _slash = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = _serviceCollection
        });

        _client.MessageCreated += CheckMember;
        _client.ComponentInteractionCreated += CheckInteraction;

        await _client.ConnectAsync();
        
        // Unregister all
        // {
        //     IReadOnlyList<DiscordApplicationCommand> i = await _client.GetGuildApplicationCommandsAsync(_guildId);
        //     foreach (DiscordApplicationCommand x in i)
        //         await _client.DeleteGuildApplicationCommandAsync(_guildId, x.Id);
        // }
        
        _slash.RegisterCommands<CreateRaid>(_guildId);
        _slash.RegisterCommands<DisbandRaid>(_guildId);
        _slash.RegisterCommands<JoinRaid>(_guildId);
        _slash.RegisterCommands<LeaveRaid>(_guildId);
        _slash.RegisterCommands<ListRaid>(_guildId);
        _slash.RegisterCommands<ReserveRaid>(_guildId);
        _slash.RegisterCommands<KickRaid>(_guildId);
        _slash.RegisterCommands<SetGuardianInfo>(_guildId);
        _slash.RegisterCommands<SetTimeRaid>(_guildId);
        _slash.RegisterCommands<StartCommand>(_guildId);
        _slash.RegisterCommands<SetRaiderRole>(_guildId);
        _slash.RegisterCommands<RoleManagement>(_guildId);
        _slash.RegisterCommands<RoleCategoryManagement>(_guildId);
        
        await Task.Delay(-1);
    }

    private async Task CheckInteraction(DiscordClient sender, ComponentInteractionCreateEventArgs componentInteraction)
    {
        if (componentInteraction.Id.StartsWith("role-select:"))
        {
            DiscordGuild guild = componentInteraction.Message.Channel.Guild;
            DiscordMember member = await guild.GetMemberAsync(componentInteraction.User.Id);
            
            string roleCategoryName = componentInteraction.Id.Replace("role-select:", "");
            RoleCategory roleCategory = await _rcRepo.GetRoleCategoryByName(roleCategoryName);
            if (roleCategory == null)
                return;

            StringBuilder sb = new StringBuilder().AppendLine("```diff");
            
            foreach (ulong option in componentInteraction.Values.Select(ulong.Parse))
            {
                DiscordRole role = guild.GetRole(option);
                
                if (member.Roles.Contains(role)) continue;
                
                await member.GrantRoleAsync(role);
                sb.AppendLine($"+ {role.Name}");
            }
            
            
            foreach (ulong option in roleCategory.Entries.Where(x => !componentInteraction.Values.Select(ulong.Parse).Contains(x.RoleId)).Select(x=>x.RoleId))
            {
                DiscordRole role = guild.GetRole(option);
                
                if (!member.Roles.Contains(role)) continue;
                
                await member.RevokeRoleAsync(role);
                sb.AppendLine($"- {role.Name}");
            }

            sb.AppendLine("```");

            DiscordEmbed embed = new DiscordEmbedBuilder()
                .WithTitle("Roles updated")
                .WithDescription(sb.ToString())
                .Build();
            await componentInteraction.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed).AsEphemeral());
        }
        else
        {
            return;
        }
    }

    private async Task CheckMember(DiscordClient sender, MessageCreateEventArgs e)
    {
        if (e.Author.IsBot) return;
        if (!await _rdRepo.Exists(e.Author.Id))
        {
            Raider rd = new(e.Author);
            await _rdRepo.Add(rd);
        }

        Raider existingRaider = await _rdRepo.Get(e.Author.Id);
        Console.WriteLine($"{existingRaider.Name} ({existingRaider.Subclass} {existingRaider.Class}) sent a message");
    }
}