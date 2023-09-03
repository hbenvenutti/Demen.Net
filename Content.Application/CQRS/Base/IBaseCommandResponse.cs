using Ether.Outcomes;

namespace Demen.Content.Application.CQRS.Base;

public interface IBaseCommandResponse<TResponseDto>
{
	public IOutcome<TResponseDto> Outcome { get; init; }
}
