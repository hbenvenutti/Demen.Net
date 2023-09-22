using Demen.Common.Enums;

namespace Demen.Application.Error;

public class InvalidDataError : ApplicationError
{
	private new const ErrorCode StatusCode = ErrorCode.BadData;
	private new const string Message = "Invalid data.";

	public InvalidDataError()
		: base(StatusCode, Message)
	{
	}
}
