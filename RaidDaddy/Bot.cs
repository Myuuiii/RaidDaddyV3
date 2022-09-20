using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using RaidDaddy.Data;

namespace RaidDaddy;

public sealed class Bot
{
    private DiscordSocketClient _client;
    private ServiceProvider _serviceCollection;
    private DataContext _db;

    private static Task Main(string[] args) => new Bot().MainAsync();

    private async Task MainAsync()
    {
        _db = new DataContext();
        _client = new DiscordSocketClient();

        _serviceCollection = new ServiceCollection()
            .AddSingleton(_db)
            .BuildServiceProvider();

        var token = Environment.GetEnvironmentVariable("token");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}