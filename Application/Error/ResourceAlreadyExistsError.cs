namespace Demen.Application.Error;

public class ResourceAlreadyExistsError
{
	public string Message {get; init;}

	public ResourceAlreadyExistsError(string resource)
	{
		Message = $"{resource} already exists.";
	}
}
