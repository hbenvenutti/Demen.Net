using System.Diagnostics.CodeAnalysis;
using Demen.Data.Repositories;
using Demen.Domain.Content.Channel;
using Demen.Domain.Content.Video;
using Demen.Domain.Management.Email;
using Demen.Domain.Management.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace Demen.Bootstrap.Bootstrapping;

[ExcludeFromCodeCoverage]
public static class RepositoryBootstrap
{
	public static void ConfigureRepositories(
		this IServiceCollection services
	)
	{
		services.AddScoped<IManagerRepository, ManagerRepository>();
		services.AddScoped<IEmailRepository, EmailRepository>();
		services.AddScoped<IVideoRepository, VideoRepository>();
		services.AddScoped<IChannelRepository, ChannelRepository>();

		return;
	}
}
