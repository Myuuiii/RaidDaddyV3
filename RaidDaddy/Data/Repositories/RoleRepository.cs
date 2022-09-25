using Microsoft.EntityFrameworkCore;
using RaidDaddy.Entities.Roles;

namespace RaidDaddy.Data.Repositories;

public class RoleRepository
{
    private readonly DataContext _db;

    public RoleRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<bool> RoleCategoryEntryExists(string categoryName, ulong roleId)
    {
        return await _db.Roles.Include(x=>x.Category)
            .AnyAsync(x=> x.Category.Name == categoryName && x.RoleId == roleId);
    }
    
    public async Task<RoleCategoryEntry> GetRoleCategoryEntry(string categoryName, ulong roleId)
    {
        return await _db.Roles.Include(x=>x.Category)
            .FirstOrDefaultAsync(x=>x.Category.Name == categoryName && x.RoleId == roleId);
    }
    
    public async Task CreateRoleCategoryEntry(RoleCategoryEntry entry)
    {
        await _db.Roles.AddAsync(entry);
        await _db.SaveChangesAsync();
    }
    
    public async Task UpdateRoleCategoryEntry(RoleCategoryEntry entry)
    {
        _db.Roles.Update(entry);
        await _db.SaveChangesAsync();
    }
    
    public async Task DeleteRoleCategoryEntry(RoleCategoryEntry entry)
    {
        _db.Roles.Remove(entry);
        await _db.SaveChangesAsync();
    }
}