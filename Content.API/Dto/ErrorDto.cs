using Demen.Content.Common.Helpers;

namespace Demen.Content.API.Dto;

public class ErrorDto
{
	public ICollection<string> Errors { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public ErrorDto(ICollection<string> errors)
	{
		Errors = errors;
	}

	public ErrorDto(string error)
	{
		Errors = new List<string>() { error };
	}

	// ---- methods --------------------------------------------------------- //
	public override string ToString() => JsonHelper.ToJson(this);
}
