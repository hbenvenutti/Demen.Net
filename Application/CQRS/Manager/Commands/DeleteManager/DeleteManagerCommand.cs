using System.Net;
using Demen.Application.CQRS.Manager.Commands.DeleteManager.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Common.Structs;
using Demen.Domain.Management.Manager;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.DeleteManager;

public class DeleteManagerCommand
	: IRequestHandler<DeleteManagerRequest, DeleteManagerResponse>
{
	private readonly IManagerRepository _managerRepository;

	public DeleteManagerCommand(IManagerRepository managerRepository)
	{
		_managerRepository = managerRepository;
	}

	public async Task<DeleteManagerResponse> Handle(
		DeleteManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		var manager = await _managerRepository
			.FindByIdAsync(request.RequestDto.Id);

		if (manager is null || manager.Status == Status.Deleted)
			return new DeleteManagerResponse(
				new ResponseDto<EmptyDto>(
					httpStatusCode: (int)HttpStatusCode.NotFound,
					statusCode: (int)StatusCode.ResourceNotFound,
					errorDto: new ApplicationErrorDto(
						new ResourceNotFoundError(Resources.Manager).Message
					)
				)
			);

		await _managerRepository
			.DeleteAsync(manager);

		return new DeleteManagerResponse(
			new ResponseDto<EmptyDto>(
				isSuccess: true,
				httpStatusCode: (int)HttpStatusCode.OK,
				statusCode: (int)StatusCode.Succeeded
			)
		);
	}
}
