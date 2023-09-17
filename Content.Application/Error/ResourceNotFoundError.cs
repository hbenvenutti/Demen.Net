using Demen.Content.Common.Enums;

namespace Demen.Content.Application.Error;

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
