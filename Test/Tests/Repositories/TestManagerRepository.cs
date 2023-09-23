using System.Diagnostics.CodeAnalysis;
using Demen.Common.Enums;
using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Data.Repositories;
using Demen.Domain.Management.Manager;
using Demen.Test.Mocks.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Demen.Test.Tests.Repositories;

[ExcludeFromCodeCoverage]
public class TestManagerRepository
{
	private readonly DemenContext _dbContext = ContextMockBuilder.Build();

	// ---- tests ----------------------------------------------------------- //

	[Fact]
	public async void CreateManagerTest()
	{
		var managerRepository = new ManagerRepository(_dbContext);

		var manager = await managerRepository
			.CreateAsync(ManagerDomain.Create(
				name: "John",
				surname: "Doe",
				password: "password"
			));

		Assert.NotEqual(
			expected: Guid.Empty,
			actual: manager.ExternalId
		);

		Assert.NotEqual(
			expected: 0,
			actual: manager.Id
		);

		Assert.Equal(
			expected: "John",
			actual: manager.Name
		);

		Assert.Equal(
			expected: "Doe",
			actual: manager.Surname
		);

		Assert.Equal(
			expected: Status.Active,
			actual: manager.Status
		);

		Assert.Null(manager.UpdatedAt);
		Assert.Null(manager.DeletedAt);
	}

	// ---------------------------------------------------------------------- //

	[Theory]
	[InlineData()]
	[InlineData(false)]
	public async void FindByIdTest(bool isIdValid = true)
	{
		var managerRepository = new ManagerRepository(_dbContext);

		var managerSeed = await Seed(managerRepository);

		var id = isIdValid ? managerSeed.ExternalId : Guid.NewGuid();

		var manager = await managerRepository
			.FindByIdAsync(id);

		if (!isIdValid)
		{
			Assert.Null(manager);

			return;
		}

		Assert.NotNull(manager);

		Assert.Equal(
			expected: id,
			actual: manager.ExternalId
		);

		Assert.NotEqual(
			expected: 0,
			actual: manager.Id
		);

		Assert.Equal(
			expected: "John",
			actual: manager.Name
		);

		Assert.Equal(
			expected: "Doe",
			actual: manager.Surname
		);

		Assert.Equal(
			expected: Status.Active,
			actual: manager.Status
		);

		Assert.Null(manager.UpdatedAt);
		Assert.Null(manager.DeletedAt);
	}

	// ---------------------------------------------------------------------- //

	[Fact]
	public async void DeleteManagerTest()
	{
		var managerRepository = new ManagerRepository(_dbContext);

		var managerSeed = await Seed(managerRepository);

		// _dbContext.Entry((ManagerEntity)managerSeed!).State = EntityState.Detached;

		await managerRepository
			.DeleteAsync(managerSeed);

		var manager = await managerRepository
			.FindByIdAsync(managerSeed.ExternalId);

		Assert.NotNull(manager);

		Assert.NotNull(manager.DeletedAt);
		Assert.NotNull(manager.UpdatedAt);

		Assert.Equal(
			expected: Status.Deleted,
			actual: manager.Status
		);

		Assert.Equal(
			expected: managerSeed.ExternalId,
			actual: manager.ExternalId
		);
	}

	// ---- private methods ------------------------------------------------- //

	private async Task<ManagerDomain> Seed(ManagerRepository repository)
	{
		var manager = await repository.CreateAsync(ManagerDomain.Create(
			name: "John",
			surname: "Doe",
			password: "password"
		));

		var existingEntry = _dbContext.Entry((ManagerEntity)manager!);

		if (existingEntry.State != EntityState.Detached)
			existingEntry.State = EntityState.Detached;

		return manager;
	}
}
