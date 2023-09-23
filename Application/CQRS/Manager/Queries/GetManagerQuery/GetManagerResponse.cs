using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Application.Dto;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerResponse : IBaseCommandResponse<GetManagerResponseDto>
{
	public ResponseDto<GetManagerResponseDto> ResponseDto { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public GetManagerResponse(ResponseDto<GetManagerResponseDto> responseDto)
	{
		ResponseDto = responseDto;
	}
}
