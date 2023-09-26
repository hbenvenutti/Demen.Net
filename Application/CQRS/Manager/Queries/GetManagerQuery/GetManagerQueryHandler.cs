using System.Net;
using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Common.Structs;
using Demen.Domain.Management.Manager;
using MediatR;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerQueryHandler :
	IRequestHandler<GetManagerRequest, Response<GetManagerResponseDto>>
{
	// ---- fields ---------------------------------------------------------- //

	private readonly IManagerRepository _managerRepository;

	// ---- constructors ---------------------------------------------------- //

	public GetManagerQueryHandler(IManagerRepository managerRepository)
	{
		_managerRepository = managerRepository;
	}

	// ---- methods --------------------------------------------------------- //

	public async Task<Response<GetManagerResponseDto>> Handle(
		GetManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		var managerDomain = await _managerRepository
			.FindByIdAsync(request.Id);

		if (managerDomain is null)
			return new Response<GetManagerResponseDto>(
				httpStatusCode: (int)HttpStatusCode.NotFound,
				statusCode: (int)StatusCode.ResourceNotFound,
				errorDto: new ApplicationErrorDto(
					new ResourceNotFoundError(Resources.Manager).Message
				)
			);

		var responseDto = (GetManagerResponseDto)managerDomain;

		return new Response<GetManagerResponseDto>(
			isSuccess: true,
			httpStatusCode: (int)HttpStatusCode.OK,
			statusCode: (int)StatusCode.Succeeded,
			data: responseDto
		);
	}
}
