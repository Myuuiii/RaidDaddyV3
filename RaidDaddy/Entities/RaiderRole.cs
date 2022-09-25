using DSharpPlus.Entities;

namespace RaidDaddy.Entities;

public class RaiderRole
{ 
    public ulong Id;
    public string Name;
    public DiscordColor Color;

    public RaiderRole()
    {
        
    }

    public RaiderRole(DiscordRole role)
    {
        this.Id = role.Id;
        this.Name = role.Name;
        this.Color = role.Color;
    }
}