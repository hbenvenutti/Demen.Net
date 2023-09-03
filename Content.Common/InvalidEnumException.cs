namespace Demen.Content.Common;

public class InvalidEnumException : ArgumentException
{
	private const string message = "Enum value cannot be null";

	public InvalidEnumException() : base(message)
	{
	}
}
