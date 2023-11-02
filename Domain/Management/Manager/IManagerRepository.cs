namespace Demen.Domain.Management.Manager;

public interface IManagerRepository
{
	Task<ManagerDomain> CreateAsync(ManagerDomainDto domain);
	Task DeleteAsync(ManagerDomain domain);
	Task<ManagerDomain?> FindByIdAsync(Guid id);
}
