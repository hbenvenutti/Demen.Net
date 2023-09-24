using System.Diagnostics.CodeAnalysis;
using Demen.Common.Enums;
using Demen.Domain.Management.Manager;

namespace Demen.Test.Mocks.Repositories;

[ExcludeFromCodeCoverage]
public class ManagerRepositoryMock : IManagerRepository
{
	private static int _id = 1;
	private readonly ICollection<ManagerDomain> _managers =
		new List<ManagerDomain>();

	public Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain)
	{
		var newDomain = new ManagerDomain()
		{
			Id = _id++,
			ExternalId = Guid.NewGuid(),
			Status = Status.Active,
			Name = managerDomain.Name,
			Surname = managerDomain.Surname,
			Password = managerDomain.Password,
			CreatedAt = DateTime.Now,
			UpdatedAt = null,
			DeletedAt = null,
			Emails = null
		};

		_managers.Add(newDomain);

		return Task.FromResult(newDomain);
	}

	public Task DeleteAsync(ManagerDomain domain)
	{
		return Task.CompletedTask;
	}

	public Task<ManagerDomain?> FindByIdAsync(Guid id)
	{
		var manager = _managers
			.FirstOrDefault(manager => manager.ExternalId == id);

		return Task.FromResult(manager);
	}

	// ---- seeds ----------------------------------------------------------- //

	public async Task<ManagerDomain> Seed()
	{
		var managerDomain = ManagerDomain.Create(
			name: "John",
			surname: "doe",
			password: "password"
		);

		return await CreateAsync(managerDomain);
	}
}
