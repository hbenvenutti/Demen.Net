using Demen.Content.Common.Enums;
using Demen.Content.Common.Helpers;

namespace Demen.Content.Data.Entities;

public abstract class BaseEntity
{
	public int Id { get; set; }
	public Guid ExternalId { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }

	public string StatusString { get; private set; }

	public required Status Status
	{
		get => StatusString.StringToEnum<Status>();

		set => StatusString = value.EnumToString();
	}
}
