using System.Diagnostics.CodeAnalysis;
using Demen.Domain.Management.Email;

namespace Demen.Test.Mocks.Repositories;

[ExcludeFromCodeCoverage]
public class EmailRepositoryMock : IEmailRepository
{
	private readonly ICollection<EmailDomain> _emails = new List<EmailDomain>();

	// ---- write methods --------------------------------------------------- //

	public Task<EmailDomain> CreateAsync(EmailDomain domain)
	{
		_emails
			.Add(
				item: new EmailDomain(
					externalId: Guid.NewGuid(),
					managerId: domain.ManagerId,
					address: domain.Address,
					createdAt: DateTime.UtcNow
				)
			);

		return Task.FromResult(domain);
	}

	public Task DeleteAsync(EmailDomain domain)
	{
		throw new NotImplementedException();
	}

	// ---- read methods ---------------------------------------------------- //

	public Task<EmailDomain?> FindByAddressAsync(string address)
	{
		return Task.FromResult(
			_emails
				.FirstOrDefault(email => email.Address == address)
		);
	}

	public Task<EmailDomain?> FindByIdAsync(Guid id)
	{
		return Task.FromResult(
			_emails
				.FirstOrDefault(email => email.ExternalId == id)
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
