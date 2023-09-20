using System.Diagnostics.CodeAnalysis;
using Demen.Content.Common.Enums;
using Demen.Content.Domain.Manager;

namespace Demen.Content.UnitTests.Mocks.Repositories;

[ExcludeFromCodeCoverage]
public class ManagerRepositoryMock : IManagerRepository
{
	private static int _id = 1;
	private readonly ICollection<ManagerDomain> _managers =
		new List<ManagerDomain>();

	public Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain)
	{
		var newDomain = new ManagerDomain(
			id: _id++,
			externalId: Guid.NewGuid(),
			status: Status.Active,
			name: managerDomain.Name,
			surname: managerDomain.Surname,
			password: managerDomain.Password,
			createdAt: DateTime.Now,
			updatedAt: null,
			deletedAt: null,
			emails: null
		);

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
