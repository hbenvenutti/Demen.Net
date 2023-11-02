using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Content.Video;
using Demen.Domain.Management.Email;

namespace Demen.Domain.Management.Manager;

public class ManagerDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //
	public required string Name { get; init; }
	public required string Surname { get; init; }

	// ---- relationships --------------------------------------------------- //

	public ICollection<EmailDomain>? Emails { get; set; }
	public ICollection<VideoDomain>? Videos { get; set; }
}
