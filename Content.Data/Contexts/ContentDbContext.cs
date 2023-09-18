using System.Diagnostics.CodeAnalysis;
using Demen.Content.Data.Config.Entities;
using Demen.Content.Data.Entities;
using Demen.Content.Data.HashMaps;
using Microsoft.EntityFrameworkCore;

namespace Demen.Content.Data.Contexts;

[ExcludeFromCodeCoverage]
public class ContentDbContext : DbContext
{
	public required DbSet<ManagerEntity> Managers { get; set; }
	public required DbSet<EmailEntity> Emails { get; set; }

	public ContentDbContext(DbContextOptions<ContentDbContext> options)
		: base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ManagerEntityConfig());
		modelBuilder.ApplyConfiguration(new EmailEntityConfig());
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
