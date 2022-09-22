using DSharpPlus.Entities;
using RaidDaddy.Data;
using RaidDaddy.Enums;

namespace RaidDaddy.Extensions.ToEmbed;

public static class GetSubclassEmbed
{
    public static DiscordEmbed ToEmbed(this Destiny2Subclass subclass)
    {
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            .WithTitle("Subclass changed")
            .WithDescription($"Your subclass has been changed to {subclass}.")
            .WithThumbnail($"{Urls.Base}{subclass}_subclass.png");
        return embed.Build();
    }
}