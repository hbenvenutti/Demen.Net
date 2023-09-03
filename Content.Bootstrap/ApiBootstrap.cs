using Demen.Content.Bootstrap.Bootstrapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demen.Content.Bootstrap;

public static class ApiBootstrap
{
	public static IServiceCollection ConfigureApi(
		this IServiceCollection services,
		IConfiguration configuration
	)
	{
		services.ConfigureDbContext(configuration);

		services.AddControllers();
		services.AddEndpointsApiExplorer();

		services.AddSwaggerGen();

		return services;
	}

	public static IApplicationBuilder ConfigureApi(
		this IApplicationBuilder app,
		IWebHostEnvironment environment
	)
	{
		if (environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

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
