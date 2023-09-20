using Demen.Data.Repositories;
using Demen.Domain.Email;
using Demen.Domain.Management.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace Demen.Bootstrap.Bootstrapping;

public static class RepositoryBootstrap
{
	public static IServiceCollection ConfigureRepositories(
		this IServiceCollection services
	)
	{
		services.AddScoped<IManagerRepository, ManagerRepository>();
		services.AddScoped<IEmailRepository, EmailRepository>();

		return services;
	}
}
