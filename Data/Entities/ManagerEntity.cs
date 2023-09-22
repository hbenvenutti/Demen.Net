using Demen.Domain.Content;
using Demen.Domain.Management.Email;
using Demen.Domain.Management.Manager;

namespace Demen.Data.Entities;

public class ManagerEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //
	public required string Name { get; set; }
	public required string Surname { get; set; }
	public required string Password { get; set; }

	// ---- relationships --------------------------------------------------- //

	public ICollection<EmailEntity>? Emails { get; set; } =
		new List<EmailEntity>();

	public ICollection<VideoEntity>? Videos { get; set; } =
		new List<VideoEntity>();

	// ---- factories ------------------------------------------------------- //
	public static implicit operator ManagerDomain?(ManagerEntity? managerEntity)
	{
		if (managerEntity is null) return null;

		var emails = managerEntity.Emails?.Count > 0
			? managerEntity
				.Emails
				.Select(email =>
				{
					email.Manager = null;
					return (EmailDomain)email!;
				})
				.ToList()
			: null;

		var videos = managerEntity.Videos?.Count > 0
			? managerEntity
				.Videos
				.Select(video =>
				{
					video.Manager = null;
					return (VideoDomain)video!;
				})
				.ToList()
			: null;

		return new ManagerDomain(
			id: managerEntity.Id,
			externalId: managerEntity.ExternalId,
			status: managerEntity.Status,
			createdAt: managerEntity.CreatedAt,
			updatedAt: managerEntity.UpdatedAt,
			deletedAt: managerEntity.DeletedAt,
			name: managerEntity.Name,
			surname: managerEntity.Surname,
			password: managerEntity.Password,
			emails: emails,
			videos: videos
		);
	}

	public static implicit operator ManagerEntity?(ManagerDomain? managerDomain)
	{
		if (managerDomain is null)
			return null;

		return new ManagerEntity
		{
			Id = managerDomain.Id,
			ExternalId = managerDomain.ExternalId,
			Status = managerDomain.Status,
			CreatedAt = managerDomain.CreatedAt,
			UpdatedAt = managerDomain.UpdatedAt,
			DeletedAt = managerDomain.DeletedAt,
			Name = managerDomain.Name,
			Surname = managerDomain.Surname,
			Password = managerDomain.Password,
			Emails = null,
			Videos = null
		};
	}
}
