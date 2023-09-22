namespace Demen.Application.Error;

public struct ResourceNotFoundError : IApplicationError
{
	public string Message {get; init;}

	public ResourceNotFoundError(string resource)
	{
		Message = $"{resource} not found.";
	}
}
