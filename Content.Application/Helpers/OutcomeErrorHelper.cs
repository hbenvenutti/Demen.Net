using Ether.Outcomes;
using Ether.Outcomes.Builder;
using Demen.Content.Application.Error;

namespace Demen.Content.Application.Helpers;

public class OutcomeErrorHelper<T>
{
	public IFailureOutcomeBuilder<T> CreateOutcomeFailure(
		ApplicationError applicationError
	)
	{
		return Outcomes
			.Failure<T>()
			.WithMessage(applicationError.Message)
			.WithStatusCode(applicationError.StatusCode);
	}
}
