using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using MediatR;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerRequest : IRequest<Response<GetManagerResponseDto>>
{
	public required Guid Id { get; init; }
}
