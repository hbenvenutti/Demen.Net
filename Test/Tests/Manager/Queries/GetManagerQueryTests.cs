using System.Diagnostics.CodeAnalysis;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Common.Enums;
using Demen.Domain.Management.Manager;
using Demen.Test.Mocks.Repositories;

namespace Demen.Test.Tests.Manager.Queries;

[ExcludeFromCodeCoverage]

public class GetManagerQueryTests
{
	private readonly ManagerRepositoryMock _managerRepository;
	private readonly CancellationToken _cancellationToken = new();
	private ManagerDomain _managerDomain = null!;

	public GetManagerQueryTests()
	{
		_managerRepository = new ManagerRepositoryMock();
		Seed();
	}

	// ---- seeds ----------------------------------------------------------- //

	private async void Seed()
	{
		_managerDomain = await _managerRepository.Seed();
	}

	// ---- tests ----------------------------------------------------------- //

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async void GetManager(
		bool idExists
	)
	{
		// ---- Arrange ----------------------------------------------------- //

		_managerDomain = await _managerRepository.CreateAsync(_managerDomain);

		var requestDto = new GetManagerRequestDto(
			id: idExists
				? _managerDomain.ExternalId
				: Guid.NewGuid()
		);

		var request = new GetManagerRequest(requestDto);

		var handler = new GetManagerQueryHandler(_managerRepository);

		// ---- Act --------------------------------------------------------- //

		var response = await handler
			.Handle(request, _cancellationToken);

		// ---- Assert ------------------------------------------------------ //
		var responseDto = response.ResponseDto.Data;

		if (!idExists)
		{
			Assert.Equal(
				expected: (int)StatusCode.ResourceNotFound,
				actual: response.ResponseDto.StatusCode
			);

			return;
		}

		Assert.NotNull(responseDto);

		Assert.Equal(
			expected: _managerDomain.ExternalId,
			actual: responseDto.Id
		);

		Assert.Equal(
			expected: _managerDomain.Name,
			actual: responseDto.Name
		);

		Assert.Equal(
			expected: _managerDomain.Surname,
			actual: responseDto.Surname
		);
	}
}
