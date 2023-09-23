using Demen.Application.Dto;

namespace Demen.Application.CQRS.Base;

public interface IBaseCommandResponse<TResponseDto> where TResponseDto : class
{
	public ResponseDto<TResponseDto> ResponseDto { get; init; }
}
