using Demen.Application.Dto;

namespace Demen.Application.CQRS.Base;

public interface IBaseCommandResponse<TResponseDto> where TResponseDto : class
{
	public Response<TResponseDto> Response { get; init; }
}
