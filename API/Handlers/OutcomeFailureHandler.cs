using Demen.Common.Errors;
using Ether.Outcomes;
using Microsoft.AspNetCore.Mvc;

namespace Demen.API.Handlers;

public static class OutcomeFailureHandler
{
	public static IActionResult HandleFailure(this IOutcome outcome)
	{
		var errorDto = new ApiErrorDto(outcome.Messages);

		return new ObjectResult(errorDto)
		{
			StatusCode = outcome.StatusCode
		};
	}
}
