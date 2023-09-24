using System.Diagnostics.CodeAnalysis;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Application.CQRS.Manager.Commands.DeleteManager;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery;
using Demen.Application.CQRS.Video.Commands.CreateVideo;
using Microsoft.Extensions.DependencyInjection;

namespace Demen.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class MediatorBootstrap
{
	public static void ConfigureMediatorServices(
		this IServiceCollection services
	)
	{
		services.AddMediatR(config => config
			.RegisterServicesFromAssemblies(
				typeof(CreateManagerCommandHandler).Assembly
			)
		);

		services.AddMediatR(config => config
			.RegisterServicesFromAssemblies(
				typeof(GetManagerQueryHandler).Assembly
			)
		);

		services.AddMediatR(config => config
			.RegisterServicesFromAssemblies(
				typeof(DeleteManagerCommand).Assembly
			)
		);

		services.AddMediatR(config => config
			.RegisterServicesFromAssemblies(
				typeof(CreateVideoCommand).Assembly
			)
		);

		return;
	}
}
