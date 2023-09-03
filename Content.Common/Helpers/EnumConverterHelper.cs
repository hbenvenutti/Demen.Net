namespace Demen.Content.Common.Helpers;

public static class EnumConverterHelper
{
	public static string EnumToString<T>(this T enumValue)
	{
		return enumValue?
			    .ToString()
		       ?? throw new InvalidEnumException();
	}

	public static T StringToEnum<T>(this string enumValue)
	{
		return (T)Enum.Parse(typeof(T), enumValue);
	}
}