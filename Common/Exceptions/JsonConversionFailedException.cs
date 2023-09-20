namespace Demen.Common.Exceptions;

public class JsonConversionFailedException : Exception
{
	private new const string Message = "Json conversion failed";

	// ---- constructors ---------------------------------------------------- //
	public JsonConversionFailedException() : base(Message)
	{
	}

	public JsonConversionFailedException(Exception innerException)
		: base(Message, innerException)
	{
	}
}
