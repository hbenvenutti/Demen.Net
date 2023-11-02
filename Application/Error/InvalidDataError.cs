namespace Demen.Application.Error;

public class InvalidDataError
{
	public string Message { get; init; }

	public InvalidDataError(string property)
	{
		Message = $"{property} is invalid.";
	}
}
