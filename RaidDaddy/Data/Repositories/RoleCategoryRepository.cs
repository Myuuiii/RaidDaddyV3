using Microsoft.EntityFrameworkCore;
using RaidDaddy.Entities.Roles;

namespace RaidDaddy.Data.Repositories;

public class RoleCategoryRepository
{
    private readonly DataContext _db;

    public RoleCategoryRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<bool> RoleCategoryWithNameExists(string name)
    {
        return await _db.RoleCategories.AnyAsync(x => x.Name == name);
    }

    public async Task<RoleCategory> GetRoleCategoryByName(string name)
    {
        return await _db.RoleCategories.Include(x=>x.Entries)
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<RoleCategory>> GetRoleCategories()
    {
        return await _db.RoleCategories.Include(x=>x.Entries)
            .ToListAsync();
    }
    
    public async Task CreateRoleCategory(RoleCategory roleCategory)
    {
        await _db.RoleCategories.AddAsync(roleCategory);
        await _db.SaveChangesAsync();
    }
    
    public async Task UpdateRoleCategory(RoleCategory roleCategory)
    {
        _db.RoleCategories.Update(roleCategory);
        await _db.SaveChangesAsync();
    }
    
    public async Task DeleteRoleCategory(RoleCategory roleCategory)
    {
        _db.RoleCategories.Remove(roleCategory);
        await _db.SaveChangesAsync();
    }        
}