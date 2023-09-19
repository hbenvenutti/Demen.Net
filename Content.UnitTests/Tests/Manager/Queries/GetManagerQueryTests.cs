using System.Diagnostics.CodeAnalysis;
using Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery;
using Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Content.Common.Enums;
using Demen.Content.Domain.Manager;
using Demen.Content.UnitTests.Mocks.Repositories;

namespace Demen.Content.UnitTests.Tests.Manager.Queries;

[ExcludeFromCodeCoverage]

public class GetManagerQueryTests
{
	private readonly ManagerRepositoryMock _managerRepository;
	private readonly CancellationToken _cancellationToken = new();
	private ManagerDomain _managerDomain;

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
		var responseDto = response.Outcome.Value;

		if (!idExists)
		{
			Assert.Equal(
				expected: (int)ErrorCode.ResourceNotFound,
				actual: response.Outcome.StatusCode
			);

			return;
		}

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
