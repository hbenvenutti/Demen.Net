using System.Diagnostics.CodeAnalysis;
using Demen.Data.Config.Entities;
using Demen.Data.Entities;
using Demen.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Demen.Data.Contexts;

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
