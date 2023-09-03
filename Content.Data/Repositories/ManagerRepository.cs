using Demen.Content.Data.Contexts;
using Demen.Content.Data.Entities;
using Demen.Content.Domain.Manager;

namespace Demen.Content.Data.Repositories;

public class ManagerRepository : IManagerRepository
{
	private readonly ContentDbContext _contentDbContext;

	// ---- constructors ---------------------------------------------------- //
	public ManagerRepository(ContentDbContext contentDbContext)
	{
		_contentDbContext = contentDbContext;
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain)
	{
		ManagerEntity managerEntity = managerDomain;

		await _contentDbContext
			.Managers
			.AddAsync(managerEntity);

		await _contentDbContext
			.SaveChangesAsync();

		managerDomain = managerEntity;

		return managerDomain;
	}
}
