using RaidDaddy.Enums;

namespace RaidDaddy.Entities;

public class RaidFireteam
{
    public Guid Id { get; set; }
    public Destiny2Raid Raid { get; set; }
    public Destiny2Encounter Encounter { get; set; } = Destiny2Encounter.CLEAN;
    public List<Raider> Raiders { get; set; } = new();
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
}