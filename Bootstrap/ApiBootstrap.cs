using Demen.Bootstrap.Bootstrapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demen.Bootstrap;

public static class ApiBootstrap
{
	public static IServiceCollection ConfigureApi(
		this IServiceCollection services,
		IConfiguration configuration
	)
	{
		services.ConfigureDbContext(configuration);
		services.ConfigureMediatorServices();
		services.ConfigureRepositories();
		services.ConfigureProviders();
		services.ConfigureSwagger();
		services.AddControllers();
		services.AddEndpointsApiExplorer();

		return services;
	}

	public static IApplicationBuilder ConfigureApi(
		this IApplicationBuilder app,
		IWebHostEnvironment environment
	)
	{
		app.ConfigureSwagger(environment);

		app.ConfigureGlobalMiddlewares();

		app.UseHttpsRedirection();
		app.UseAuthorization();

		app.MigrateDatabaseOnStartUp();

		return app;
	}

	public static IEndpointRouteBuilder ConfigureControllers(
		this IEndpointRouteBuilder app
	)
	{
		app.MapControllers();
		return app;
	}

	public static IConfigurationBuilder ConfigureApi(
		this IConfigurationBuilder configuration
	)
	{
		var environment = Environment
			.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
			?? Environments.Development;

		configuration.AddJsonFile(
			path:"appsettings.json",
			optional: true
		);

		configuration.AddJsonFile(
			path: $"appsettings.{environment}.json",
			optional: true,
			reloadOnChange: true
		);

		return configuration;
	}
}
