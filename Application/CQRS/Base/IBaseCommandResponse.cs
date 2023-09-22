using Demen.Application.Dto;

namespace Demen.Application.CQRS.Base;

public interface IBaseCommandResponse<TResponseDto>
{
	public ResponseDto<TResponseDto> ResponseDto { get; init; }
}
