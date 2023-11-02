using Demen.Application.Providers.ContentProvider.Dto;

namespace Demen.Application.Providers.ContentProvider;

public interface IContentProvider
{
	public Task<ExternalVideoDto?> FetchVideoInfo(string videoId);
	public Task<ExternalChannelDto?> FetchChannelInfo(string channelId);
}
