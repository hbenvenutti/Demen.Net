using System.Diagnostics.CodeAnalysis;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Domain.Manager;
using Demen.Content.UnitTests.Mocks.Repositories;

namespace Demen.Content.UnitTests.Tests.Manager.Commands;

[ExcludeFromCodeCoverage]
public class CreateManagerCommandTests
{
	private readonly IManagerRepository _managerRepository;
	private readonly CancellationToken _cancellationToken;

	public CreateManagerCommandTests()
	{
		_managerRepository = new ManagerRepositoryMock();
		_cancellationToken = new CancellationToken();
	}

	[Fact]
	public async void CreateManager()
	{
		// ---- Arrange ----------------------------------------------------- //

		var requestDto = new CreateManagerRequestDto()
		{
			Name = "John",
			Surname = "Doe",
			Password = "password"
		};

		var request = new CreateManagerRequest(requestDto);

		var handler = new CreateManagerCommandHandler(_managerRepository);

		// ---- Act --------------------------------------------------------- //

		var result = await handler
			.Handle(request, _cancellationToken);

		var value = result.Outcome.Value;

		// ---- Assert ------------------------------------------------------ //

		Assert.Equal(
			expected: requestDto.Name,
			actual: value.Name
		);

		Assert.Equal(
			expected: requestDto.Surname,
			actual: value.Surname
		);

		Assert.NotEqual(
			expected: DateTime.MinValue,
			actual: value.CreatedAt
		);

		Assert.NotEqual(
			expected: Guid.Empty,
			actual: value.Id
		);

		Assert.IsType<Guid>(value.Id);
	}
}
