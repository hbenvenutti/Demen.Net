using Demen.Common.Enums;

namespace Demen.Domain.Base;

public abstract class BaseDomain
{
	public required int Id { get; init; }
	public required Guid ExternalId { get; init; }
	public required DateTime CreatedAt { get; init; }
	public required Status Status { get; init; }
	public required DateTime? UpdatedAt { get; init; }
	public required DateTime? DeletedAt { get; init; }
}
