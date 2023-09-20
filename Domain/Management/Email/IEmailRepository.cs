namespace Demen.Domain.Management.Email;

public interface IEmailRepository
{
	Task<EmailDomain?> FindByAddressAsync(string address);
}
