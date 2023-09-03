using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand;

namespace Demen.Content.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class MediatorBootstrap
{
	public static IServiceCollection ConfigureMediatorServices(
		this IServiceCollection services
	)
	{
		services.AddMediatR(typeof(CreateManagerCommandHandler).Assembly);

		return services;
	}
}
