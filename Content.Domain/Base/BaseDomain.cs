using Demen.Content.Common.Enums;

namespace Demen.Content.Domain.Base;

public abstract class BaseDomain
{
	public int Id { get; set; }
	public Guid ExternalId { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }

	public string StatusString => Status.ToString();
	public Status Status { get; set; }
}
