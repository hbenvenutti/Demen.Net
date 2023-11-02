using Demen.Common.Helpers;

namespace Demen.Application.Dto;

public class ApplicationErrorDto
{
	public ICollection<string> Errors { get; init; }

	// ---- constructors ---------------------------------------------------- //

	public ApplicationErrorDto(ICollection<string> errors)
	{
		Errors = errors;
	}

	public ApplicationErrorDto(string error)
	{
		Errors = new List<string>() { error };
	}

	// ---- methods --------------------------------------------------------- //

	public override string ToString() => JsonHelper.ToJson(obj: this);
}
