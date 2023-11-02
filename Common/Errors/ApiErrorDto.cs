using Demen.Common.Helpers;

namespace Demen.Common.Errors;

public class ApiErrorDto
{
	public ICollection<string> Errors { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public ApiErrorDto(ICollection<string> errors)
	{
		Errors = errors;
	}

	public ApiErrorDto(string error)
	{
		Errors = new List<string>() { error };
	}

	// ---- methods --------------------------------------------------------- //
	public override string ToString() => JsonHelper.ToJson(this);
}
