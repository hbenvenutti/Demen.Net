using Demen.Data.Contexts;
using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demen.Test.Mocks.Contexts;

public class DbContextMock : IDemenContext
{
	public DbSet<ManagerEntity>? Managers { get; set; }
	public DbSet<EmailEntity>? Emails { get; set; }
	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return Task.FromResult(0);
	}
}
