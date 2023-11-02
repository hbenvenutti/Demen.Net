using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Queries.FindVideo.Dto;
using Demen.Domain.Content.Video;

namespace Demen.Web.Providers;

public class ApiProvider : IApiProvider
{
	private readonly HttpClient _httpClient;

	public ApiProvider(IConfiguration configuration)
	{
		_httpClient = new HttpClient();
		_httpClient.BaseAddress = new Uri(configuration["Api:BaseUrl"]!);
	}

	public async Task<VideoDomain?> GetVideoAsync(Guid id)
	{
		var response = await _httpClient
			.GetFromJsonAsync<Response<FindVideoResponseDto>>(
				requestUri: $"videos/{id}"
			);

		return response?.Data?.Video ?? null;
	}
}
