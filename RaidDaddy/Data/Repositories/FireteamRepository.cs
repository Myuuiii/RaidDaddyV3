using Microsoft.EntityFrameworkCore;
using RaidDaddy.Entities;

namespace RaidDaddy.Data.Repositories;

public sealed class FireteamRepository
{
    private readonly DataContext _db;

    public FireteamRepository()
    {
        _db = new DataContext();
    }
    
    public FireteamRepository(DataContext db)
    {
        _db = db;
    }
    
    public async Task<RaidFireteam?> Get(Guid id)
    {
        return await _db.Fireteams.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<RaidFireteam?>> Get()
    {
        return await _db.Fireteams.ToListAsync();
    }

    public async Task Create(RaidFireteam fireteam)
    {
        await _db.Fireteams.AddAsync(fireteam);
        SaveChanges();
    }

    public async Task Update(RaidFireteam fireteam)
    {
        _db.Fireteams.Update(fireteam);
        SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        _db.Fireteams.Remove(await Get(id));
        SaveChanges();
    }

    private async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}