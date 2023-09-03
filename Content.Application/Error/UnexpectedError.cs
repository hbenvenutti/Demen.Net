namespace Demen.Content.Application.Error;

public class UnexpectedError : ApplicationError
{
	private static readonly int _statusCode = 500;

	// ---- constructors ---------------------------------------------------- //
	public UnexpectedError(string message)
		: base(message: message, statusCode: _statusCode)
	{
	}
}
