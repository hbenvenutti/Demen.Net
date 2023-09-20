using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demen.Data.Contexts;

public interface IDemenContext
{
	DbSet<ManagerEntity> Managers { get; set; }
	DbSet<EmailEntity> Emails { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
