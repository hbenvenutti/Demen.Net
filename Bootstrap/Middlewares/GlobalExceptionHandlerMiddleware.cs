using System.Net;
using Demen.Application.Dto;
using Demen.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace Demen.Bootstrap.Middlewares;

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

		var responseDto = new ResponseDto<EmptyDto>(
			httpStatusCode: (int)code,
			statusCode: (int)Common.Enums.StatusCode.Unexpected,
			errorDto: new ApplicationErrorDto(exception.Message)
		);

		var jsonDto = JsonHelper.ToJson(responseDto);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;

		return context.Response.WriteAsync(jsonDto);
	}
}
