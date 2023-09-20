using Demen.Common.Enums;

namespace Demen.Application.Error;

public abstract class ApplicationError
{
	// ---- properties ------------------------------------------------------ //
	public ErrorCode StatusCode { get; init; }
	public string Message { get; init; }

	// ---- constructors ---------------------------------------------------- //
	protected ApplicationError(ErrorCode statusCode, string message)
	{
		StatusCode = statusCode;
		Message = message;
	}
}
