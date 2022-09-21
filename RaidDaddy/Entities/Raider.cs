using DSharpPlus.Entities;
using RaidDaddy.Enums;

namespace RaidDaddy.Entities;

public class Raider
{
    public Raider()
    {

    }

    public Raider(DiscordUser user)
    {
        this.Id = user.Id;
        this.Name = user.Username;
    }

    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Mention => $"<@{Id}>";
    public Destiny2Class Class { get; set; }
    public Destiny2Subclass Subclass { get; set; }
    public RaidFireteam? CurrentTeam { get; set; }
}