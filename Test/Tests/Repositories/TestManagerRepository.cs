using System.Diagnostics.CodeAnalysis;
using Demen.Common.Enums;
using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Data.Repositories;
using Demen.Domain.Management.Manager;
using Demen.Test.Mocks.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Demen.Test.Tests.Repositories;

[ExcludeFromCodeCoverage]
public class TestManagerRepository
{
	private readonly DemenContext _dbContext = ContextMockBuilder.Build();
	private readonly ManagerRepository _managerRepository;
	private readonly ManagerDomain _managerSeed;

	public TestManagerRepository()
	{
		_managerRepository = new ManagerRepository(_dbContext);
		_managerSeed = Seed().Result;

		_dbContext.ChangeTracker.Entries().ToList().Clear();

	}

	// ---- tests ----------------------------------------------------------- //

	[Fact]
	public async void CreateManagerTest()
	{
		var manager = await _managerRepository
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
		var id = isIdValid ? _managerSeed.ExternalId : Guid.NewGuid();

		var manager = await _managerRepository
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

		Assert.Equal(
			expected: _managerSeed.Id,
			actual: manager.Id
		);

		Assert.Equal(
			expected: _managerSeed.Name,
			actual: manager.Name
		);

		Assert.Equal(
			expected: _managerSeed.Surname,
			actual: manager.Surname
		);

		Assert.Equal(
			expected: _managerSeed.Status,
			actual: manager.Status
		);

		Assert.Equal(
			expected: _managerSeed.CreatedAt,
			actual: manager.CreatedAt
		);

		Assert.Equal(
			expected: _managerSeed.UpdatedAt,
			actual: manager.UpdatedAt
		);

		Assert.Equal(
			expected: _managerSeed.DeletedAt,
			actual: manager.DeletedAt
		);
	}

	// ---------------------------------------------------------------------- //

	[Fact]
	public async void DeleteManagerTest()
	{
		await _managerRepository
			.DeleteAsync(_managerSeed);

		var manager = await _managerRepository
			.FindByIdAsync(_managerSeed.ExternalId);

		Assert.NotNull(manager);

		Assert.NotNull(manager.DeletedAt);
		Assert.NotNull(manager.UpdatedAt);

		Assert.Equal(
			expected: Status.Deleted,
			actual: manager.Status
		);

		Assert.Equal(
			expected: _managerSeed.ExternalId,
			actual: manager.ExternalId
		);
	}

	// ---- private methods ------------------------------------------------- //

	private async Task<ManagerDomain> Seed()
	{
		var manager = await _managerRepository.CreateAsync(ManagerDomain.Create(
			name: "John",
			surname: "Doe",
			password: "password"
		));

		_dbContext
			.ChangeTracker
			.Entries()
			.ToList()
			.ForEach(entry => entry.State = EntityState.Detached);

		return manager;
	}
}
