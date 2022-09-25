using System.Text.Json;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RaidDaddy.Entities;

namespace RaidDaddy.Data.Repositories;

public class RaiderRoleRepository
{
    private const string FileName = "./resource/raiderRole.json";
    private RaiderRole _role;

    public RaiderRoleRepository()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FileName)!);
        if (!File.Exists(FileName)) File.WriteAllText(FileName, JsonSerializer.Serialize(new RaiderRole()));
        _role = JsonSerializer.Deserialize<RaiderRole>(File.ReadAllText((FileName)));
    }

    public ulong GetId()
    {
        return _role.Id;
    }

    public string GetName()
    {
        return _role.Name;
    }

    public DiscordColor GetColor()
    {
        return _role.Color;
    }

    public void Set(DiscordRole role)
    {
        this._role = new RaiderRole(role);
    }
}