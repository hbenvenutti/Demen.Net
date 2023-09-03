namespace Demen.Content.Application.Error;

public class ResourceNotFoundError : ApplicationError
{
	private const int _statusCode = 404;

	public ResourceNotFoundError(string resource)
		: base(
			_statusCode,
			message: $"{resource} not found."
		)
	{
	}
}
