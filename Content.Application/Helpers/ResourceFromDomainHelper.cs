namespace Demen.Content.Application.Helpers;

public static class ResourceFromDomainHelper
{
	private const int _removableLength = 6;

	public static string GetResource(this string domainString)
	{
		var resource = domainString[..^_removableLength];

		return resource;
	}
}
