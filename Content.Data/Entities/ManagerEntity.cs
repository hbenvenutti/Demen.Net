namespace Demen.Content.Data.Entities;

public class ManagerEntity : BaseEntity
{
	public required string Name { get; set; }
	public required string Surname { get; set; }
	public required string Password { get; set; }
}
