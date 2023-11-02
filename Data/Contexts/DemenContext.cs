using Demen.Data.Config.Entities;
using Demen.Data.Entities;
using Demen.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Demen.Data.Contexts;

public sealed class DemenContext : DbContext, IDemenContext
{
	public DbSet<ManagerEntity> Managers { get; set; }
	public DbSet<EmailEntity> Emails { get; set; }
	public DbSet<VideoEntity> Videos { get; set; }
	public DbSet<ChannelEntity> Channels { get; set; }

	public DemenContext(DbContextOptions<DemenContext> options)
		: base(options)
	{
		Managers = Set<ManagerEntity>();
		Emails = Set<EmailEntity>();
		Videos = Set<VideoEntity>();
		Channels = Set<ChannelEntity>();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ManagerEntityConfig());
		modelBuilder.ApplyConfiguration(new EmailEntityConfig());
		modelBuilder.ApplyConfiguration(new VideoEntityConfig());
		modelBuilder.ApplyConfiguration(new ChannelEntityConfig());
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
