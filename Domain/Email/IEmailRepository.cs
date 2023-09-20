namespace Demen.Domain.Email;

public interface IEmailRepository
{
	Task<EmailDomain?> FindByAddressAsync(string address);
}
