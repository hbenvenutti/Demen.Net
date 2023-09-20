using Demen.Common.Enums;
using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Domain.Management.Manager;
using Microsoft.EntityFrameworkCore;

namespace Demen.Data.Repositories;

public class ManagerRepository : IManagerRepository
{
	private readonly IDemenContext _dbContext;

	// ---- constructors ---------------------------------------------------- //
	public ManagerRepository(IDemenContext dbContext)
	{
		_dbContext = dbContext;
	}

	// ---- write methods --------------------------------------------------- //

	public async Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain)
	{
		var managerEntity = (ManagerEntity)managerDomain!;

		await _dbContext
			.Managers
			.AddAsync(managerEntity);

		return (ManagerDomain)managerEntity!;
	}

	public async Task DeleteAsync(ManagerDomain managerDomain)
	{
		var managerEntity = (ManagerEntity)managerDomain!;

		managerEntity.Status = Status.Deleted;
		managerEntity.DeletedAt = DateTime.UtcNow;

		_dbContext
			.Managers
			.Update(managerEntity);

		await _dbContext
			.SaveChangesAsync();

		return;
	}

	// ---- read methods ---------------------------------------------------- //

	public async Task<ManagerDomain?> FindByIdAsync(Guid id)
	{
		var managerEntity = await _dbContext
			.Managers
			.AsNoTracking()
			.SingleOrDefaultAsync(manager => manager.ExternalId == id);

		return (ManagerDomain?)managerEntity;
	}
}