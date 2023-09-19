using Demen.Content.Common.Enums;

namespace Demen.Content.Application.Error;

public class EmailInUSeError : ApplicationError
{
	private new const ErrorCode StatusCode = ErrorCode.BadData;
	private new const string Message = "Email is already in use.";

	public EmailInUSeError()
		: base(StatusCode, Message)
	{
	}
}
