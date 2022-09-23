using RaidDaddy.Enums;
using RaidDaddy.Modules.Raid;

namespace RaidDaddy.Extensions;

public static class RaidEnumToString
{
    public static string ToRaidString(this CreateRaid.Raid raid) => raid switch
    {
        CreateRaid.Raid.Kf => "Kings Fall",
        CreateRaid.Raid.Lw => "Last Wish",
        CreateRaid.Raid.Dsc => "Deep Stone Crypt",
        CreateRaid.Raid.Gos => "Garden of Salvation",
        CreateRaid.Raid.Vog => "Vault of Glass",
        CreateRaid.Raid.Votd => "Vow of the Disciple",
        _ => throw new ArgumentOutOfRangeException(nameof(raid), raid, null)
    };
}