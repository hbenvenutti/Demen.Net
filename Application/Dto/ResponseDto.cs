namespace Demen.Application.Dto;

public class ResponseDto<T>
{
	public bool IsSuccess { get; }
	public int HttpStatusCode { get; }
	public int StatusCode { get; }
	public ApplicationErrorDto? Error { get; }
	public T? Data { get; }

	// ---- constructors ---------------------------------------------------- //

	public ResponseDto(
		int httpStatusCode,
		int statusCode,
		T? data,
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
