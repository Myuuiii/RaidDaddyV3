using Microsoft.EntityFrameworkCore;
using RaidDaddy.Entities;

namespace RaidDaddy.Data.Repositories;

public sealed class RaiderRepository
{
    private readonly DataContext _db;

    public RaiderRepository()
    {
        _db = new DataContext();
    }

    public RaiderRepository(DataContext db)
    {
        _db = db;
    }
    
    public async Task<List<Raider>> Get()
    {
        return await _db.Raiders.ToListAsync();
    }
    
    public async Task<Raider?> Get(int id)
    {
        return await _db.Raiders.FindAsync(id);
    }

    public async Task Create(Raider raider)
    {
        await _db.Raiders.AddAsync(raider);
        await SaveChanges();
    }
    
    public async Task Update(Raider raider)
    {
        _db.Raiders.Update(raider);
        await SaveChanges();
    }
    
    public async Task Delete(Raider raider)
    {
        _db.Raiders.Remove(raider);
        await SaveChanges();
    }

    private async Task SaveChanges()
    {
        await _db.SaveChangesAsync();
    }
}