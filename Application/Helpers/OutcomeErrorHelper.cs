using Demen.Application.Error;
using Ether.Outcomes;
using Ether.Outcomes.Builder;

namespace Demen.Application.Helpers;

public class OutcomeErrorHelper<T>
{
	public IFailureOutcomeBuilder<T> CreateOutcomeFailure(
		ApplicationError applicationError
	)
	{
		return Outcomes
			.Failure<T>()
			.WithMessage(applicationError.Message)
			.WithStatusCode((int)applicationError.StatusCode);
	}
}
