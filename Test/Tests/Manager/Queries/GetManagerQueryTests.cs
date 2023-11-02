using System.Diagnostics.CodeAnalysis;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery;
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

		var request = new GetManagerRequest()
		{
			Id = idExists
				? _managerDomain.ExternalId
				: Guid.NewGuid()
		};

		var handler = new GetManagerQueryHandler(_managerRepository);

		// ---- Act --------------------------------------------------------- //

		var response = await handler
			.Handle(request, _cancellationToken);

		// ---- Assert ------------------------------------------------------ //
		var responseDto = response.Data;

		if (!idExists)
		{
			Assert.Equal(
				expected: StatusCode.ResourceNotFound,
				actual: response.StatusCode
			);

			return;
		}

		Assert.NotNull(responseDto);

		Assert.Equal(
			expected: _managerDomain.ExternalId,
			actual: responseDto.Id
		);

		Assert.Equal(
			expected: _managerDomain.Status.ToString(),
			actual: responseDto.Status
		);

		Assert.Equal(
			expected: _managerDomain.Name,
			actual: responseDto.Name
		);

		Assert.Equal(
			expected: _managerDomain.CreatedAt,
			actual: responseDto.CreatedAt
		);

		Assert.Equal(
			expected: _managerDomain.UpdatedAt,
			actual: responseDto.UpdatedAt
		);

		Assert.Equal(
			expected: _managerDomain.DeletedAt,
			actual: responseDto.DeletedAt
		);

		Assert.Equal(
			expected: _managerDomain.Surname,
			actual: responseDto.Surname
		);
	}
}
