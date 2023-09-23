using Demen.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Demen.Test.Mocks.Contexts;

public static class ContextMockBuilder
{
	private static readonly DbContextOptionsBuilder<DemenContext> DbOptions =
		new DbContextOptionsBuilder<DemenContext>()
		.UseInMemoryDatabase(databaseName: new Guid().ToString());

	public static DemenContext Build()
	{
		return new DemenContext(DbOptions.Options)
		{ };
	}
}
