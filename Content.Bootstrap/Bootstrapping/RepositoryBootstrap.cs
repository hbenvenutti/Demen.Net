using Demen.Content.Data.Repositories;
using Demen.Content.Domain.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace Demen.Content.Bootstrap.Bootstrapping;

public static class RepositoryBootstrap
{
	public static IServiceCollection ConfigureRepositories(
		this IServiceCollection services
	)
	{
		services.AddScoped<IManagerRepository, ManagerRepository>();

		return services;
	}
}
