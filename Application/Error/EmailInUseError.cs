using Demen.Common.Enums;

namespace Demen.Application.Error;

public class EmailInUseError : ApplicationError
{
	private new const ErrorCode StatusCode = ErrorCode.BadData;
	private new const string Message = "Email is already in use.";

	public EmailInUseError()
		: base(StatusCode, Message)
	{
	}
}
