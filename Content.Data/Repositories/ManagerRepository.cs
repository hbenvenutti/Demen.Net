using Demen.Content.Common.Enums;
using Demen.Content.Data.Contexts;
using Demen.Content.Data.Entities;
using Demen.Content.Domain.Manager;
using Microsoft.EntityFrameworkCore;

namespace Demen.Content.Data.Repositories;

public class ManagerRepository : IManagerRepository
{
	private readonly ContentDbContext _contentDbContext;

	// ---- constructors ---------------------------------------------------- //
	public ManagerRepository(ContentDbContext contentDbContext)
	{
		_contentDbContext = contentDbContext;
	}

	// ---- write methods --------------------------------------------------- //

	public async Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain)
	{
		var managerEntity = (ManagerEntity)managerDomain!;

		await _contentDbContext
			.Managers
			.AddAsync(managerEntity);

		await _contentDbContext
			.SaveChangesAsync();

		return (ManagerDomain)managerEntity!;
	}

	public async Task DeleteAsync(ManagerDomain managerDomain)
	{
		var managerEntity = (ManagerEntity)managerDomain!;

		managerEntity.Status = Status.Deleted;
		managerEntity.DeletedAt = DateTime.UtcNow;

		_contentDbContext
			.Managers
			.Update(managerEntity);

		await _contentDbContext
			.SaveChangesAsync();

		return;
	}

	// ---- read methods ---------------------------------------------------- //

	public async Task<ManagerDomain?> FindByIdAsync(Guid id)
	{
		var managerEntity = await _contentDbContext
			.Managers
			.AsNoTracking()
			.SingleOrDefaultAsync(manager => manager.ExternalId == id);

		return (ManagerDomain?)managerEntity;
	}
}
