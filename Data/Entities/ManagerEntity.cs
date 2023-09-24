using Demen.Domain.Content.Video;
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

	public required ICollection<EmailEntity>? Emails { get; set; } =
		new List<EmailEntity>();

	public required ICollection<VideoEntity>? Videos { get; set; } =
		new List<VideoEntity>();

	// ---- factories ------------------------------------------------------- //
	public static implicit operator ManagerDomain?(ManagerEntity? managerEntity)
	{
		if (managerEntity is null) return null;

		var emails = managerEntity.Emails?.Any() ?? false
			? managerEntity.Emails
				.Select(email =>
				{
					email.Manager = null;
					return (EmailDomain)email!;
				})
				.ToList()
			: null;

		var videos = managerEntity.Videos?.Any() ?? false

			? managerEntity.Videos
				.Select(video =>
				{
					video.Manager = null;
					return (VideoDomain)video!;
				})
				.ToList()

			: null;

		return new ManagerDomain()
		{
			Id = managerEntity.Id,
			ExternalId = managerEntity.ExternalId,
			Status = managerEntity.Status,
			CreatedAt = managerEntity.CreatedAt,
			UpdatedAt = managerEntity.UpdatedAt,
			DeletedAt = managerEntity.DeletedAt,
			Name = managerEntity.Name,
			Surname = managerEntity.Surname,
			Password = managerEntity.Password,
			Emails = emails,
			Videos = videos
		};
	}

	public static implicit operator ManagerEntity(ManagerDomain managerDomain)
		=> new()
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
