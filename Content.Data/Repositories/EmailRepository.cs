using Demen.Content.Data.Contexts;
using Demen.Content.Domain.Email;
using Microsoft.EntityFrameworkCore;

namespace Demen.Content.Data.Repositories;

public class EmailRepository : IEmailRepository
{
	private readonly ContentDbContext _dbContext;

	public EmailRepository(ContentDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	// ---- read methods ---------------------------------------------------- //

	public async Task<EmailDomain?> FindByAddressAsync(string address)
	{
		var emailEntity = await _dbContext.Emails
			.AsNoTracking()
			.SingleOrDefaultAsync(email => email.Address == address);

		return (EmailDomain?)emailEntity;
	}
}
