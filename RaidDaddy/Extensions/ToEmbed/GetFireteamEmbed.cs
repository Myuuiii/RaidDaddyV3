using System.Text;
using DSharpPlus.Entities;
using RaidDaddy.Entities;

namespace RaidDaddy.Extensions.ToEmbed;

public static class GetFireteamEmbed
{
    public static DiscordEmbed ToEmbed(this RaidFireteam fireteam)
    {
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            .WithTitle($"{fireteam.Raid.Name} Raid")
            .WithImageUrl(fireteam.Raid.BannerImg)
            .WithTimestamp(fireteam.Date)
            .WithColor(fireteam.Raid.Color)
            .AddField("Member Count", $"{fireteam.Raiders.Count}/6");

        StringBuilder sb = new();
        sb.AppendLine("**Members**");
        foreach (Raider raider in fireteam.Raiders)
            sb.AppendLine($"- {raider.Mention} ~ {raider.Subclass} {raider.Class}");
        sb.AppendLine($"\n**Completed in**: {fireteam.Time}");
        embed.WithDescription(sb.ToString());
        return embed.Build();
    }
}