using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;

namespace Demen.Content.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class MediatorBootstrap
{
	public static IServiceCollection ConfigureMediatorServices(
		this IServiceCollection services
	)
	{

		services.AddMediatR(config => config
			.RegisterServicesFromAssemblies(typeof(CreateManagerCommandHandler)
				.Assembly
			)
		);

		return services;
	}
}
