using Demen.Common.Enums;

namespace Demen.Data.Entities;

public abstract class BaseEntity
{
	public required int Id { get; set; }
	public required Guid ExternalId { get; set; }
	public required DateTime CreatedAt { get; set; }
	public required DateTime? UpdatedAt { get; set; }
	public required DateTime? DeletedAt { get; set; }
	public required Status Status { get; set;}
}
