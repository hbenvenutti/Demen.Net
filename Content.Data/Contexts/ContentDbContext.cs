using Microsoft.EntityFrameworkCore;

namespace Content.Data.Contexts;

public class ContentDbContext : DbContext
{
	public ContentDbContext(DbContextOptions<ContentDbContext> options)
		: base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
