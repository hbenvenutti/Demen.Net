using System.Diagnostics.CodeAnalysis;
using Demen.Domain.Management.Email;

namespace Demen.Test.Mocks.Repositories;

[ExcludeFromCodeCoverage]
public class EmailRepositoryMock : IEmailRepository
{
	private readonly ICollection<EmailDomain> _emails = new List<EmailDomain>();

	public Task<EmailDomain?> FindByAddressAsync(string address)
	{
		return Task.FromResult(
			_emails
				.FirstOrDefault(email => email.Address == address)
		);
	}

	// ---- seeds ----------------------------------------------------------- //

	public Task<EmailDomain> Seed(string emailAddress)
	{
		var emailDomain = EmailDomain.Create(
			managerId:0,
			address: emailAddress
		);

		_emails.Add(emailDomain);

		return Task.FromResult(emailDomain);
	}
}
