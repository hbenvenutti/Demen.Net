namespace Demen.Content.Domain.Base;

public interface IRepository<TDomain>
{
	Task<TDomain> CreateAsync(TDomain domain);
	Task DeleteAsync(TDomain domain);
	Task<TDomain?> FindByIdAsync(Guid id);
}
