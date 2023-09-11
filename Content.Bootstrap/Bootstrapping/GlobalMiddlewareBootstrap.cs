using Demen.Content.Bootstrap.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Demen.Content.Bootstrap.Bootstrapping;

public static class GlobalMiddlewareBootstrap
{
	public static void ConfigureGlobalMiddlewares(this IApplicationBuilder app)
	{
		app
			.UseMiddleware<GlobalExceptionHandlerMiddleware>();
	}
}
