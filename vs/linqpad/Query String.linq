<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	int? errorCode = GetQueryString<int?>(null);
	errorCode.Dump();
}

// Define other methods and classes here
public static T GetQueryString<T>(string value, T defaultValue = default(T))
{
	if (string.IsNullOrWhiteSpace(value))
	{
		return defaultValue;
	}

	var type = typeof(T);
	var underlyingType = Nullable.GetUnderlyingType(type);

	type = underlyingType ?? type;

	try
	{
		if (type.IsEnum)
		{
			return (T)Enum.Parse(type, value);
		}

		return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
	}
	catch
	{
		return defaultValue;
	}
}
