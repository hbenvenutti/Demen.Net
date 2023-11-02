namespace Demen.Domain.Base;

public interface IRepository<TDomain>
{
	Task<TDomain> CreateAsync(TDomain domain);
	Task DeleteAsync(TDomain domain);

}
