namespace Demen.Application.Utils;

public interface IUnityOfWork
{
	Task CommitAsync(CancellationToken cancellationToken = default);
}
