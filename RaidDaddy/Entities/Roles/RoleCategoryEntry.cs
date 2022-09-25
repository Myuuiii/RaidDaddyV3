namespace RaidDaddy.Entities.Roles;

public class RoleCategoryEntry
{
    public Guid Id { get; set; }
    public ulong RoleId { get; set; }
    public string Description { get; set; }
    public RoleCategory Category { get; set; }
}