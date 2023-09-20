using Ether.Outcomes;

namespace Demen.Application.CQRS.Base;

public interface IBaseCommandResponse<TResponseDto>
{
	public IOutcome<TResponseDto> Outcome { get; init; }
}
