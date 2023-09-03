using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Demen.Content.Data.Config.Entities;
using Demen.Content.Data.HashMaps;
using Demen.Content.Data.Entities;

namespace Demen.Content.Data.Contexts;

[ExcludeFromCodeCoverage]
public class ContentDbContext : DbContext
{
	public required DbSet<ManagerEntity> Managers { get; set; }

	public ContentDbContext(DbContextOptions<ContentDbContext> options)
		: base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ManagerEntityConfig());
	}

	public override Task<int> SaveChangesAsync(
		CancellationToken cancellationToken = new()
	)
	{
		OnSaveChanges();

		return base
			.SaveChangesAsync(cancellationToken);
	}

	private void OnSaveChanges()
	{
		foreach (var entry in ChangeTracker.Entries())
		{
			if (entry.Entity is BaseEntity)
				entry.HandleOnSaveChanges();
		}
	}
}
