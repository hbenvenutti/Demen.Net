using Demen.Data.Contexts;
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

	// ---- read methods ---------------------------------------------------- //

	public async Task<EmailDomain?> FindByAddressAsync(string address)
	{
		var emailEntity = await _dbContext.Emails
			.AsNoTracking()
			.SingleOrDefaultAsync(email => email.Address == address);

		return (EmailDomain?)emailEntity;
	}
}
