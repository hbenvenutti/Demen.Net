namespace Demen.Content.Domain.Base;

public interface IRepository<TDomain>
{
	Task<TDomain> CreateAsync(TDomain domain);
}
