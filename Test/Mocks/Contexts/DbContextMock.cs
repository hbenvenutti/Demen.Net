using System.Diagnostics.CodeAnalysis;
using Demen.Data.Contexts;
using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demen.Test.Mocks.Contexts;

[ExcludeFromCodeCoverage]
public class DbContextMock : IDemenContext
{
	public DbSet<ManagerEntity> Managers { get; set; } = null!;
	public DbSet<EmailEntity> Emails { get; set; } = null!;
	public DbSet<VideoEntity> Videos { get; set; } = null!;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return Task.FromResult(0);
	}
}
