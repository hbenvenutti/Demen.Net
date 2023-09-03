namespace Demen.Content.Common.Exceptions;

public class InvalidEnumException : ArgumentException
{
	private const string message = "Enum value cannot be null";

	public InvalidEnumException() : base(message)
	{
	}
}
