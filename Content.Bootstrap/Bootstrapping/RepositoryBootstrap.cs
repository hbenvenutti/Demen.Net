using Demen.Content.Domain.Email;
using Demen.Content.Domain.Manager;
using Demen.Data.Repositories;
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
