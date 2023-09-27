using Demen.Domain.Base;

namespace Demen.Domain.Management.Manager;

public interface IManagerRepository : IRepository<ManagerDomain>
{
	Task<ManagerDomain?> FindByIdAsync(Guid id);
}
