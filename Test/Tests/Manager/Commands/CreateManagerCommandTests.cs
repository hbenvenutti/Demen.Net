using System.Diagnostics.CodeAnalysis;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Common.Enums;
using Demen.Test.Mocks.Repositories;

namespace Demen.Test.Tests.Manager.Commands;

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
		await _emailRepository
			.Seed(ExistentEmail);
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
			managerRepository: _managerRepository,
			emailRepository: _emailRepository
		);

		// ---- Act --------------------------------------------------------- //

		var result = await handler
			.Handle(request, _cancellationToken);

		var responseDto = result.ResponseDto.Data;

		// ---- Assert ------------------------------------------------------ //

		if (emailInUse)
		{
			Assert.Equal(
				expected: (int)StatusCode.Conflict,
				actual: result.ResponseDto.StatusCode
			);

			return;
		}

		if (invalidEmailType)
		{
			Assert.Equal(
				expected: (int)StatusCode.InvalidData,
				actual: result.ResponseDto.StatusCode
			);

			return;
		}

		Assert.NotNull(responseDto);

		Assert.Equal(
			expected: requestDto.Name,
			actual: responseDto.Name
		);

		Assert.Equal(
			expected: requestDto.Surname,
			actual: responseDto.Surname
		);

		Assert.IsType<DateTime>(responseDto.CreatedAt);

		Assert.IsType<Guid>(responseDto.Id);
	}
}
