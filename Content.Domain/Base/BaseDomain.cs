using Demen.Content.Common.Enums;

namespace Demen.Content.Domain.Base;

public abstract class BaseDomain
{
	public int Id { get; protected init; }
	public Guid ExternalId { get; protected init; }
	public DateTime CreatedAt { get; protected init; }
	public DateTime? UpdatedAt { get; protected init; }
	public DateTime? DeletedAt { get; protected init; }
	public Status Status { get; protected init; }
}
