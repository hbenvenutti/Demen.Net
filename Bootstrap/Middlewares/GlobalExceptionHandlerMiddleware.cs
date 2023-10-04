using System.Net;
using Demen.Application.CQRS.Base;
using Demen.Application.Dto;
using Demen.Common.Enums;
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
		const HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

		var messages = exception.InnerException is null
			? new List<string>() { exception.Message }

			: new List<string>()
			{
				exception.Message, exception.InnerException.Message
			};

		var responseDto = new Response<EmptyDto>(
			httpStatusCode: httpStatusCode,
			statusCode: StatusCode.Unexpected,
			errorDto: new ApplicationErrorDto(messages)
		);

		var jsonDto = JsonHelper.ToJson(responseDto);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)httpStatusCode;

		return context.Response.WriteAsync(jsonDto);
	}
}
