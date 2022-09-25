using Microsoft.EntityFrameworkCore;
using RaidDaddy.Entities;
using RaidDaddy.Entities.Roles;
using SmartEnum.EFCore;

namespace RaidDaddy.Data;

public class DataContext : DbContext
{
    private const string _connectionString = "Host=192.168.178.20;Port=3307;Database=raiddaddy;User=myuu";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureSmartEnum();
    }

    public DbSet<Raider> Raiders { get; set; }
    public DbSet<RaidFireteam?> Fireteams { get; set; }
    public DbSet<RoleCategory> RoleCategories { get; set; }
    public DbSet<RoleCategoryEntry> Roles { get; set; }
}