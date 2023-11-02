using Demen.Domain.Base;

namespace Demen.Domain.Management.Email;

public interface IEmailRepository : IRepository<EmailDomain>
{
	Task<EmailDomain?> FindByIdAsync(Guid id);
	Task<EmailDomain?> FindByAddressAsync(string address);
}
