using System.Net;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Common.Structs;
using Demen.Domain.Management.Manager;
using MediatR;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerQueryHandler :
	IRequestHandler<GetManagerRequest, GetManagerResponse>
{
	// ---- fields ---------------------------------------------------------- //

	private readonly IManagerRepository _managerRepository;

	// ---- constructors ---------------------------------------------------- //

	public GetManagerQueryHandler(IManagerRepository managerRepository)
	{
		_managerRepository = managerRepository;
	}

	// ---- methods --------------------------------------------------------- //

	public async Task<GetManagerResponse> Handle(
		GetManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		var managerDomain = await _managerRepository
			.FindByIdAsync(request.RequestDto.Id);

		if (managerDomain is null)
			return new GetManagerResponse(
				new ResponseDto<GetManagerResponseDto>(
					httpStatusCode: (int)HttpStatusCode.NotFound,
					statusCode: (int)StatusCode.ResourceNotFound,
					errorDto: new ApplicationErrorDto(
						new ResourceNotFoundError(Resources.Manager).Message
					),
					data: null
				)
			);

		var responseDto = (GetManagerResponseDto)managerDomain;

		return new GetManagerResponse(
			new ResponseDto<GetManagerResponseDto>(
				isSuccess: true,
				httpStatusCode: (int)HttpStatusCode.Accepted,
				statusCode: (int)StatusCode.Succeeded,
				data: responseDto
			)
		);
	}
}
