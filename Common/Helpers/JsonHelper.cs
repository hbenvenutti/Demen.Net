using System.Text.Json;
using Demen.Common.Exceptions;

namespace Demen.Common.Helpers;

public static class JsonHelper
{
	// ---- fields ---------------------------------------------------------- //
	private static readonly JsonSerializerOptions _options = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		PropertyNameCaseInsensitive = true,
		WriteIndented = true
	};

	// ---- methods --------------------------------------------------------- //
	public static string ToJson<T>(T obj)
	{
		return JsonSerializer.Serialize(obj, options: _options);
	}

	public static T FromJson<T>(string json)
	{
		return JsonSerializer.Deserialize<T>(json)
		    ?? throw new JsonConversionFailedException();
	}
}
