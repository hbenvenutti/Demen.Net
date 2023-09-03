namespace Demen.Content.Common.Exceptions;

public class JsonConversionFailedException : Exception
{
	private static readonly string _message = "Json conversion failed";

	// ---- constructors ---------------------------------------------------- //
	public JsonConversionFailedException() : base(_message)
	{
	}

	public JsonConversionFailedException(Exception innerException)
		: base(_message, innerException)
	{
	}
}
