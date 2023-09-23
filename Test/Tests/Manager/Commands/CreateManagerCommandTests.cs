using System.Diagnostics.CodeAnalysis;
using System.Net;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Error;
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

		var responseDtoData = result.ResponseDto.Data;

		// ---- Assert ------------------------------------------------------ //

		if (emailInUse)
		{
			Assert.False(result.ResponseDto.IsSuccess);

			Assert.Equal(
				expected: (int)StatusCode.Conflict,
				actual: result.ResponseDto.StatusCode
			);

			Assert.Equal(
				expected: (int)HttpStatusCode.BadRequest,
				actual: result.ResponseDto.HttpStatusCode
			);

			Assert.NotNull(result.ResponseDto.Error);

			Assert.Equal(
				expected: EmailInUseError.Message,
				actual: result.ResponseDto.Error.Errors.First()
			);

			Assert.Null(result.ResponseDto.Data);


			return;
		}

		if (invalidEmailType)
		{
			Assert.False(result.ResponseDto.IsSuccess);

			Assert.Equal(
				expected: (int)StatusCode.InvalidData,
				actual: result.ResponseDto.StatusCode
			);

			Assert.Equal(
				expected: (int)HttpStatusCode.BadRequest,
				actual: result.ResponseDto.HttpStatusCode
			);

			Assert.Null(result.ResponseDto.Data);

			Assert.NotNull(result.ResponseDto.Error);

			Assert.Equal(
				expected:
					new InvalidDataError(
						property: nameof(requestDto.EmailType)
					).Message,
				actual: result.ResponseDto.Error.Errors.First()
			);

			return;
		}

		Assert.NotNull(responseDtoData);

		Assert.Null(result.ResponseDto.Error);

		Assert.True(result.ResponseDto.IsSuccess);

		Assert.Equal(
			expected: (int)HttpStatusCode.Created,
			actual: result.ResponseDto.HttpStatusCode
		);

		Assert.Equal(
			expected: (int)StatusCode.Succeeded,
			actual: result.ResponseDto.StatusCode
		);

		Assert.Equal(
			expected: requestDto.Name,
			actual: responseDtoData.Name
		);

		Assert.Equal(
			expected: requestDto.Surname,
			actual: responseDtoData.Surname
		);

		Assert.IsType<DateTime>(responseDtoData.CreatedAt);

		Assert.IsType<Guid>(responseDtoData.Id);
	}
}
