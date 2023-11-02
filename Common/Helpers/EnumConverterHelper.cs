using Demen.Common.Exceptions;

namespace Demen.Common.Helpers;

public static class EnumConverterHelper
{
	public static string EnumToString<T>(this T enumValue) =>
		enumValue?.ToString()
	       ?? throw new InvalidEnumException();

	public static T StringToEnum<T>(this string value) where T : struct, Enum
	{
		var result = Enum.TryParse<T>(
			value: value,
			ignoreCase: true,
			result: out var convertedEnum
		);

		if (!result) throw new InvalidEnumException();

		return convertedEnum;
	}

	public static bool IsEnum<T>(this string enumValue) =>
		Enum.IsDefined(typeof(T), enumValue);
}
