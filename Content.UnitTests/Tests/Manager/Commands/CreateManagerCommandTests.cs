using System.Diagnostics.CodeAnalysis;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Common.Enums;
using Demen.Content.UnitTests.Mocks.Repositories;

namespace Demen.Content.UnitTests.Tests.Manager.Commands;

[ExcludeFromCodeCoverage]
public class CreateManagerCommandTests
{
	private readonly ManagerRepositoryMock _managerRepository = new ();
	private readonly EmailRepositoryMock _emailRepository = new();
	private readonly CancellationToken _cancellationToken = new();

	private const string ExistentEmail = "existent@email";
	private const string InvalidEmailType = "InvalidEmailType";

	// ---- constructor ----------------------------------------------------- //

	public CreateManagerCommandTests()
	{
		Seed();
	}

	// ---- seeds ----------------------------------------------------------- //

	private async void Seed()
	{
		await _emailRepository.Seed(ExistentEmail);
	}

	// ---- tests ----------------------------------------------------------- //

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	[InlineData(false, false, "Personal")]
	[InlineData(false, false, "Corporate")]
	[InlineData(false, true)]
	public async void CreateManager(
		bool emailInUse,
		bool invalidEmailType = false,
		string? emailType = null
	)
	{
		// ---- Arrange ----------------------------------------------------- //

		var requestDto = new CreateManagerRequestDto()
		{
			Name = "John",
			Surname = "Doe",
			Password = "password",
			Email = !emailInUse
				? "johndoe@email.com"
				: ExistentEmail,
			EmailType = !invalidEmailType
				? emailType
				: InvalidEmailType
		};

		var request = new CreateManagerRequest(requestDto);

		var handler = new CreateManagerCommandHandler(
			_managerRepository,
			_emailRepository
		);

		// ---- Act --------------------------------------------------------- //

		var result = await handler
			.Handle(request, _cancellationToken);

		var responseDto = result.Outcome.Value;

		// ---- Assert ------------------------------------------------------ //

		if (emailInUse || invalidEmailType)
		{
			Assert.Equal(
				expected: (int)ErrorCode.BadData,
				actual: result.Outcome.StatusCode
			);

			return;
		}

		Assert.Equal(
			expected: requestDto.Name,
			actual: responseDto.Name
		);

		Assert.Equal(
			expected: requestDto.Surname,
			actual: responseDto.Surname
		);

		Assert.NotEqual(
			expected: DateTime.MinValue,
			actual: responseDto.CreatedAt
		);

		Assert.NotEqual(
			expected: Guid.Empty,
			actual: responseDto.Id
		);

		Assert.IsType<Guid>(responseDto.Id);
	}
}
