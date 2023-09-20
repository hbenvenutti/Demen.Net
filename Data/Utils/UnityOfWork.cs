using Demen.Application.Utils;
using Demen.Data.Contexts;

namespace Demen.Data.Utils;

public class UnityOfWork : IUnityOfWork
{
	private readonly IDemenContext _dbContext;

	public UnityOfWork(IDemenContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		await _dbContext
			.SaveChangesAsync(cancellationToken);
	}
}
