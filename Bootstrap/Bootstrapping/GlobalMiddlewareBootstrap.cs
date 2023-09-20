using Demen.Bootstrap.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Demen.Bootstrap.Bootstrapping;

public static class GlobalMiddlewareBootstrap
{
	public static void ConfigureGlobalMiddlewares(this IApplicationBuilder app)
	{
		app
			.UseMiddleware<GlobalExceptionHandlerMiddleware>();
	}
}
