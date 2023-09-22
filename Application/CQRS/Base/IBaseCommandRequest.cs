using MediatR;

namespace Demen.Application.CQRS.Base;

public interface IBaseCommandRequest<out TResponse, TDto> : IRequest<TResponse>
{
	TDto RequestDto { get; init; }
}
