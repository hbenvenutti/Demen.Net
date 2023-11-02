namespace Demen.Application.Error;

public struct ResourceNotFoundError
{
	public string Message {get; init;}

	public ResourceNotFoundError(string resource)
	{
		Message = $"{resource} not found.";
	}
}
