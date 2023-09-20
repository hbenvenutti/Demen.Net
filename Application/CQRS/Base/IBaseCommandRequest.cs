using MediatR;

namespace Demen.Application.CQRS.Base;

public interface IBaseCommandRequest<T, TDto> : IRequest<T>
{
	public TDto RequestDto { get; init; }
}
