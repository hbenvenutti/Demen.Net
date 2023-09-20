namespace Demen.Application.Helpers;

public static class ResourceFromDomainHelper
{
	private const int RemovableLength = 6;

	public static string GetResource(this string domainString)
	{
		var resource = domainString[..^RemovableLength];

		return resource;
	}
}
