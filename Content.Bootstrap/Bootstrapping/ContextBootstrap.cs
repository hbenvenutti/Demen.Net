using Demen.Content.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demen.Content.Bootstrap.Bootstrapping;

public static class ContextBootstrap
{
	public static IServiceCollection ConfigureDbContext(
		this IServiceCollection services,
		IConfiguration configuration
	)
	{
		var contentConnectionString = configuration
			.GetConnectionString(name: "Content");

		services.AddDbContext<ContentDbContext>(options => options
			.UseNpgsql(contentConnectionString)
		);

		return services;
	}

	public static void MigrateDatabaseOnStartUp(this IApplicationBuilder builder)
	{
		using var scope = builder
			.ApplicationServices
			.CreateAsyncScope();

		using var contentContext = scope
			.ServiceProvider
			.GetRequiredService<ContentDbContext>();

		contentContext
			.Database
			.Migrate();
	}
}
