namespace Demen.Content.Application.Error;

public abstract class ApplicationError
{
	// ---- properties ------------------------------------------------------ //
	public int StatusCode { get; init; }
	public string Message { get; init; }

	// ---- constructors ---------------------------------------------------- //
	protected ApplicationError(int statusCode, string message)
	{
		StatusCode = statusCode;
		Message = message;
	}
}
