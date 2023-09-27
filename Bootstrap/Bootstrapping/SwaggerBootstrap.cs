using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Demen.Bootstrap.Bootstrapping;

public static class SwaggerBootstrap
{
	public static void ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen();

		services.AddSwaggerGen(options =>
			options.SwaggerDoc(
				name: "v1",
				info: new OpenApiInfo { Title = "Demen API", Version = "v1" }
			)
		);

		return;
	}

	public static void ConfigureSwagger(
		this IApplicationBuilder app,
		IWebHostEnvironment environment
	)
	{
		if (environment.IsDevelopment())
		{
			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/")
				{
					context.Response.Redirect(location: "/swagger");
					return;
				}

				await next();
			});

			app.UseSwagger();
			app.UseSwaggerUI();
		}

		return;
	}
}
