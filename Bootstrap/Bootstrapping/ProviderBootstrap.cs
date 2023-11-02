using Demen.Application.Providers.ContentProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Demen.Bootstrap.Bootstrapping;

public static class ProviderBootstrap
{
	public static void ConfigureProviders(
		this IServiceCollection services
	)
	{
		services.AddScoped<IContentProvider, YoutubeProvider>();

		return;
	}
}
