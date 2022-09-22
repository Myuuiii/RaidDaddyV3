using DSharpPlus.Entities;
using RaidDaddy.Data;
using RaidDaddy.Enums;

namespace RaidDaddy.Extensions.ToEmbed;

public static class GetClassEmbed
{
    public static DiscordEmbed ToEmbed(this Destiny2Class d2Class)
    {
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            .WithTitle("Class changed")
            .WithDescription($"Your class has been changed to {d2Class}.")
            .WithThumbnail($"{Urls.Base}{d2Class}_class.png");
        return embed.Build();
    }
}