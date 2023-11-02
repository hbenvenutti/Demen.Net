using System.Diagnostics.CodeAnalysis;
using System.Net;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Test.Mocks.Repositories;

namespace Demen.Test.Tests.Manager.Commands;

[ExcludeFromCodeCoverage]
public class CreateManagerCommandTests
{
	private readonly ManagerRepositoryMock _managerRepository = new();
	private readonly EmailRepositoryMock _emailRepository = new();
	private readonly CancellationToken _cancellationToken = new();

	private const string ExistentEmail = "existent@email";
	private const string InvalidEmailType = "InvalidEmailType";

	// ---- constructor ----------------------------------------------------- //

	public CreateManagerCommandTests() => Seed();

	// ---- seeds ----------------------------------------------------------- //

	private async void Seed() => await _emailRepository.Seed(ExistentEmail);

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

		var request = new CreateManagerRequest()
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

		var handler = new CreateManagerCommandHandler(
			managerRepository: _managerRepository,
			emailRepository: _emailRepository
		);

		// ---- Act --------------------------------------------------------- //

		var result = await handler
			.Handle(request, _cancellationToken);

		var responseDtoData = result.Data;

		// ---- Assert ------------------------------------------------------ //

		if (emailInUse)
		{
			Assert.False(result.IsSuccess);

			Assert.Equal(
				expected: StatusCode.Conflict,
				actual: result.StatusCode
			);

			Assert.Equal(
				expected: HttpStatusCode.Conflict,
				actual: result.HttpStatusCode
			);

			Assert.NotNull(result.Error);

			Assert.Equal(
				expected: EmailInUseError.Message,
				actual: result.Error.Errors.First()
			);

			Assert.Null(result.Data);

			return;
		}

		if (invalidEmailType)
		{
			Assert.False(result.IsSuccess);

			Assert.Equal(
				expected: StatusCode.InvalidData,
				actual: result.StatusCode
			);

			Assert.Equal(
				expected: HttpStatusCode.BadRequest,
				actual: result.HttpStatusCode
			);

			Assert.Null(result.Data);

			Assert.NotNull(result.Error);

			Assert.Equal(
				expected: new InvalidDataError(
					property: nameof(request.EmailType)
				).Message,

				actual: result.Error.Errors.First()
			);

			return;
		}

		Assert.NotNull(responseDtoData);

		Assert.Null(result.Error);

		Assert.True(result.IsSuccess);

		Assert.Equal(
			expected: HttpStatusCode.Created,
			actual: result.HttpStatusCode
		);

		Assert.Equal(
			expected: StatusCode.Succeeded,
			actual: result.StatusCode
		);

		Assert.Equal(
			expected: request.Name,
			actual: responseDtoData.Name
		);

		Assert.Equal(
			expected: request.Surname,
			actual: responseDtoData.Surname
		);

		Assert.IsType<DateTime>(responseDtoData.CreatedAt);

		Assert.IsType<Guid>(responseDtoData.Id);
	}
}
