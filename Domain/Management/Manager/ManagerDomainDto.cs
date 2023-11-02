using Demen.Domain.Base;
using Demen.Domain.Content.Video;
using Demen.Domain.Management.Email;
using Demen.Common.Enums;

namespace Demen.Domain.Management.Manager;

public class ManagerDomainDto : BaseDomain
{
	// ---- properties ------------------------------------------------------ //
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public required string Password { get; init; }

	// ---- relationships --------------------------------------------------- //

	public ICollection<EmailDomain>? Emails { get; init; }
	public ICollection<VideoDomain>? Videos { get; init; }

	// ---- factories ------------------------------------------------------- //

	public static ManagerDomainDto Create(
		string name,
		string surname,
		string password
	) =>
		new()
		{
			Id = 0,
			ExternalId = Guid.Empty,
			Status = Status.Active,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = null,
			DeletedAt = null,
			Name = name,
			Surname = surname,
			Password = password,
			Emails = null,
			Videos = null
		};
}
