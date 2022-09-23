using System;
using RaidDaddy.Enums;
using RaidDaddy.Modules.Raid;

namespace RaidDaddy.Extensions;

public static class RaidEnumConverter
{
    public static Destiny2Raid ToDestiny2Raid(this CreateRaid.Raid raid) => raid switch
    {
        CreateRaid.Raid.Kf => Destiny2Raid.KF,
        CreateRaid.Raid.Lw => Destiny2Raid.LW,
        CreateRaid.Raid.Dsc => Destiny2Raid.DSC ,
        CreateRaid.Raid.Gos => Destiny2Raid.GOS,
        CreateRaid.Raid.Vog => Destiny2Raid.VOG,
        CreateRaid.Raid.Votd => Destiny2Raid.VOTD,
        _ => throw new ArgumentOutOfRangeException(nameof(raid), raid, null)
    };
}