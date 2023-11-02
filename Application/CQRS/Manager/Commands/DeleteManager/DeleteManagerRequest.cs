using Demen.Application.CQRS.Base;
using Demen.Application.Dto;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.DeleteManager;

public class DeleteManagerRequest : IRequest<Response<EmptyDto>>
{
	public required Guid Id { get; init; }
}
