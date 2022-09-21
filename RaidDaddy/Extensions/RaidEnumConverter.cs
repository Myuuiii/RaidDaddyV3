using RaidDaddy.Enums;
using RaidDaddy.Modules.Raid;

namespace RaidDaddy.Extensions;

public static class RaidEnumConverter
{
    public static Destiny2Raid ToDestiny2Raid(this CreateRaid.Raid raid)
    {
        return raid switch
        {
            CreateRaid.Raid.KF => Destiny2Raid.KF,
            CreateRaid.Raid.LW => Destiny2Raid.LW,
            CreateRaid.Raid.DSC => Destiny2Raid.DSC ,
            CreateRaid.Raid.GOS => Destiny2Raid.GOS,
            CreateRaid.Raid.VOG => Destiny2Raid.VOG,
            CreateRaid.Raid.VOTD => Destiny2Raid.VOTD,
        };
    }
}