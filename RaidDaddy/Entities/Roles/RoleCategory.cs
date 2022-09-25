namespace RaidDaddy.Entities.Roles;

public class RoleCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool AllowMultiSelect { get; set; }
    public List<RoleCategoryEntry> Entries { get; set; } = new();
}