using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Data.Helpers;
using Demen.Domain.Management.Email;
using Microsoft.EntityFrameworkCore;

namespace Demen.Data.Repositories;

public class EmailRepository : IEmailRepository
{
	private readonly IDemenContext _dbContext;

	public EmailRepository(IDemenContext dbContext)
	{
		_dbContext = dbContext;
	}

	// ---- write methods --------------------------------------------------- //

	public async Task<EmailDomain> CreateAsync(EmailDomain emailDomain)
	{
		var emailEntity = (EmailEntity)emailDomain;

		await _dbContext.Emails.AddAsync(emailEntity);

		await _dbContext.SaveChangesAsync();

		return (EmailDomain)emailEntity;
	}

	public Task DeleteAsync(EmailDomain emailDomain)
	{
		var emailEntity = (EmailEntity)emailDomain;

		emailEntity.DeleteEntity();

		_dbContext.Emails.Update(emailEntity);

		return Task.CompletedTask;
	}

	// ---- read methods ---------------------------------------------------- //

	public async Task<EmailDomain?> FindByAddressAsync(string address)
	{
		var emailEntity = await _dbContext.Emails
			.AsNoTracking()
			.SingleOrDefaultAsync(email => email.Address == address);

		if (emailEntity is null) return null;

		return (EmailDomain)emailEntity;
	}

	public async Task<EmailDomain?> FindByIdAsync(Guid id)
	{
		var emailEntity = await _dbContext.Emails
			.AsNoTracking()
			.SingleOrDefaultAsync(email => email.ExternalId == id);

		if (emailEntity is null) return null;

		return (EmailDomain)emailEntity;
	}
}
