using Demen.Content.Common.Enums;

namespace Demen.Content.Application.Error;

public class UnexpectedError : ApplicationError
{
	private new const ErrorCode StatusCode = ErrorCode.Unexpected;

	// ---- constructors ---------------------------------------------------- //
	public UnexpectedError(string message)
		: base(message: message, statusCode: StatusCode)
	{
	}
}
