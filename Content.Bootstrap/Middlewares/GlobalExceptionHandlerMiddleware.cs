using System.Net;
using Demen.Content.Common.Errors;
using Demen.Content.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace Demen.Content.Bootstrap.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public GlobalExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}

		catch (Exception exception)
		{
			await HandleException(context, exception);
		}
	}

	private static Task HandleException(
		HttpContext context,
		Exception exception
	)
	{
		const HttpStatusCode code = HttpStatusCode.InternalServerError;

		var errorDto = new ApiErrorDto(exception.Message);
		var jsonDto = JsonHelper.ToJson(errorDto);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int) code;

		return context.Response.WriteAsync(jsonDto);
	}
}
