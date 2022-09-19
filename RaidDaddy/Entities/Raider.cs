using RaidDaddy.Enums;

namespace RaidDaddy.Entities;

public class Raider
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Mention => $"<@{Id}>";
    public Destiny2Class Class { get; set; }
    public Destiny2Subclass Subclass { get; set; }
}