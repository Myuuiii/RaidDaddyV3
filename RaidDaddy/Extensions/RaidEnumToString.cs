using RaidDaddy.Modules.Raid;

namespace RaidDaddy.Extensions;

public static class RaidEnumToString
{
    public static string ToRaidString(this CreateRaid.Raid raid) => raid switch
    {
        CreateRaid.Raid.KF => "Kings Fall",
        CreateRaid.Raid.LW => "Last Wish",
        CreateRaid.Raid.DSC => "Deep Stone Crypt",
        CreateRaid.Raid.GOS => "Garden of Salvation",
        CreateRaid.Raid.VOG => "Vault of Glass",
        CreateRaid.Raid.VOTD => "Vow of the Disciple",
    };
}