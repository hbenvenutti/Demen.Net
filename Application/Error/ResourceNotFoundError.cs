using Demen.Common.Enums;

namespace Demen.Application.Error;

public class ResourceNotFoundError : ApplicationError
{
	private new const ErrorCode StatusCode = ErrorCode.ResourceNotFound;

	public ResourceNotFoundError(string resource)
		: base(
			StatusCode,
			message: $"{resource} not found."
		)
	{
	}
}
