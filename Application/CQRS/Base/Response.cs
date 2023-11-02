using System.Net;
using Demen.Application.Dto;
using Demen.Common.Enums;

namespace Demen.Application.CQRS.Base;

public class Response<T> where T : class
{
	public bool IsSuccess { get; }
	public HttpStatusCode HttpStatusCode { get; }
	public StatusCode StatusCode { get; }
	public ApplicationErrorDto? Error { get; }
	public T? Data { get; }

	// ---- constructors ---------------------------------------------------- //

	public Response(
		HttpStatusCode httpStatusCode,
		StatusCode statusCode,
		T? data = null,
		bool isSuccess = false,
		ApplicationErrorDto? errorDto = null
	)
	{
		IsSuccess = isSuccess;
		HttpStatusCode = httpStatusCode;
		StatusCode = statusCode;
		Error = errorDto;
		Data = data;
	}
}
