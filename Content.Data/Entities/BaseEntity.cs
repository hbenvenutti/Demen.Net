using Demen.Common.Enums;

namespace Demen.Data.Entities;

public abstract class BaseEntity
{
	public int Id { get; set; }
	public Guid ExternalId { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
	public Status Status { get; set;}
}
