using Ardalis.SmartEnum;
using DSharpPlus.Entities;

namespace RaidDaddy.Enums;

public abstract class Destiny2Raid : SmartEnum<Destiny2Raid>
{
    #region Values

    public static readonly Destiny2Raid GOS = new GardenOfSalvation("Garden of Salvation", 0);
    public static readonly Destiny2Raid LW = new LastWish("Last Wish", 1);
    public static readonly Destiny2Raid DSC = new DeepStoneCrypt("Deep Stone Crypt", 2);
    public static readonly Destiny2Raid VOTD = new VowOfTheDisciple("Vow of the Disciple", 3);
    public static readonly Destiny2Raid VOG = new VaultOfGlass("Vault of Glass", 5);
    public static readonly Destiny2Raid KF = new KingsFall("King's Fall", 6);
    
    #endregion
    
    #region Properties
    
    public string Name { get;set; }
    public abstract DiscordColor Color { get; }
    public abstract string BannerImg { get; }
    public abstract List<string> Translations { get; }
    public abstract List<Destiny2Encounter> Encounters { get; }
    
    #endregion

    public Destiny2Raid(string name, int value) : base(name, value)
    {
        Name = name;
    }
    
    #region Sealed Classes

    private sealed class GardenOfSalvation : Destiny2Raid
    {
        public GardenOfSalvation(string name, int value) : base(name,value)
        {
        }

        public override DiscordColor Color => DiscordColor.DarkGreen;
        public override string BannerImg => "";
        public override List<string> Translations => new() { "gos", "garden", "gardenofsalvation" };
        public override List<Destiny2Encounter> Encounters => new()
        {
            Destiny2Encounter.GOS_ConquerSanctified,
            Destiny2Encounter.GOS_DefeatConsecrated,
            Destiny2Encounter.GOS_EvadeConsecrated,
            Destiny2Encounter.GOS_SummonConsecrated
        };
    }
    
    private sealed class LastWish : Destiny2Raid
    {
        public LastWish(string name, int value) : base(name,value)
        {
        }

        public override DiscordColor Color => DiscordColor.Magenta;
        public override string BannerImg => "";
        public override List<string> Translations => new() { "lw", "lastwish" };
        public override List<Destiny2Encounter> Encounters => new()
        {
            Destiny2Encounter.LW_Kalli,
            Destiny2Encounter.LW_Morgeth,
            Destiny2Encounter.LW_Queenswalk,
            Destiny2Encounter.LW_Riven,
            Destiny2Encounter.LW_Vault,
            Destiny2Encounter.LW_ShuroChi
        };
    }
    
    private sealed class DeepStoneCrypt : Destiny2Raid
    {
        public DeepStoneCrypt(string name, int value) : base(name,value)
        {
        }

        public override DiscordColor Color => DiscordColor.Blue;
        public override string BannerImg => "";
        public override List<string> Translations => new() { "dsc", "crypt", "deepstonecrypt"};
        public override List<Destiny2Encounter> Encounters => new()
        {
            Destiny2Encounter.DSC_Atraks,
            Destiny2Encounter.DSC_Blizzard,
            Destiny2Encounter.DSC_Descent,
            Destiny2Encounter.DSC_Taniks,
            Destiny2Encounter.DSC_SecurityBreach,
            Destiny2Encounter.DSC_SpaceWalk
        };
    }
    
    private sealed class VowOfTheDisciple : Destiny2Raid
    {
        public VowOfTheDisciple(string name, int value) : base(name,value)
        {
        }

        public override DiscordColor Color => DiscordColor.Blue;
        public override string BannerImg => "";
        public override List<string> Translations => new() { "votd", "vow", "disciple", "vowofthedisciple"};
        public override List<Destiny2Encounter> Encounters => new()
        {
            Destiny2Encounter.VOTD_Artifacts,
            Destiny2Encounter.VOTD_Caretaker,
            Destiny2Encounter.VOTD_Obelisk,
            Destiny2Encounter.VOTD_Payload,
            Destiny2Encounter.VOTD_Rhulk
        };
    }
    
    private sealed class VaultOfGlass : Destiny2Raid
    {
        public VaultOfGlass(string name, int value) : base(name,value)
        {
        }

        public override DiscordColor Color => DiscordColor.Gold;
        public override string BannerImg => "";
        public override List<string> Translations => new() { "vault", "vog", "vaultofglass" };
        public override List<Destiny2Encounter> Encounters => new()
        {
            Destiny2Encounter.VOG_Atheon,
            Destiny2Encounter.VOG_Conflux,
            Destiny2Encounter.VOG_Entrance,
            Destiny2Encounter.VOG_Gatekeeper,
            Destiny2Encounter.VOG_Oracles,
            Destiny2Encounter.VOG_Templar,
            Destiny2Encounter.VOG_GorgonLabyrinth,
            Destiny2Encounter.VOG_JumpingPuzzle
        };
    }
    
    private sealed class KingsFall : Destiny2Raid
    {
        public KingsFall(string name, int value) : base(name,value)
        {
        }

        public override DiscordColor Color => DiscordColor.DarkRed;
        public override string BannerImg => "";
        public override List<string> Translations => new() { "kf", "fall", "kingsfall", "king" };
        public override List<Destiny2Encounter> Encounters => new()
        {
            Destiny2Encounter.KF_Entrance,
            Destiny2Encounter.KF_Golgoroth,
            Destiny2Encounter.KF_Oryx,
            Destiny2Encounter.KF_Relics,
            Destiny2Encounter.KF_Totems,
            Destiny2Encounter.KF_Warpriest,
            Destiny2Encounter.KF_JumpingPuzzle,
            Destiny2Encounter.KF_DaughtersOfOryx
        };
    }
    
    #endregion
}