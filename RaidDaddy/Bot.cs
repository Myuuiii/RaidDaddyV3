using System.Reflection;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using RaidDaddy.Data;
using RaidDaddy.Data.Repositories;
using RaidDaddy.Modules.Raid;

namespace RaidDaddy;

public sealed class Bot
{
    private ServiceProvider _serviceCollection;
    private DataContext _db;
    private DiscordClient _client;
    private SlashCommandsExtension? _slash;
    
    private FireteamRepository _ftRepo;
    private RaiderRepository _rdRepo;
    
    private const ulong _guildId = 887198526579281920;

    private static Task Main(string[] args) => new Bot().MainAsync();

    private async Task MainAsync()
    {
        _db = new DataContext();
        _ftRepo = new FireteamRepository(_db);        
        _rdRepo = new RaiderRepository(_db);
        _client = new DiscordClient(new DiscordConfiguration()
        {
            Token = Environment.GetEnvironmentVariable("token"),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.All
        });
        
        _serviceCollection = new ServiceCollection()
            .AddSingleton(_ftRepo)
            .AddSingleton(_rdRepo)
            .BuildServiceProvider();

        _slash = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = _serviceCollection
        });

        await _client.ConnectAsync();
        
        _slash.RegisterCommands<CreateRaid>(_guildId);
        _slash.RegisterCommands<DisbandRaid>(_guildId);
        _slash.RegisterCommands<JoinRaid>(_guildId);
        _slash.RegisterCommands<LeaveRaid>(_guildId);
        _slash.RegisterCommands<ListRaid>(_guildId);
        _slash.RegisterCommands<ReserveRaid>(_guildId);
        _slash.RegisterCommands<KickRaid>(_guildId);

        await Task.Delay(-1);
    }
}