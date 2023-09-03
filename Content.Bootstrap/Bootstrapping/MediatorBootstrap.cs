using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand;

namespace Demen.Content.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class MediatorBootstrap
{
	public static IServiceCollection ConfigureMediatorServices(
		this IServiceCollection services
	)
	{
		// services.AddMediatR(config => config
		// 	.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
		// );

		services.AddMediatR(config => config
			.RegisterServicesFromAssemblies(typeof(CreateManagerCommandHandler).Assembly)
		);

		// services.AddMediatR(typeof(CreateManagerCommandHandler));

		return services;
	}
}
