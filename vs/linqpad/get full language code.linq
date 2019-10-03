<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	GetCultureName("cs").Dump();
}

/// <summary>
/// A map of language code without region to language code with region
/// </summary>
Dictionary<string, string> LanguageCodeMap = new Dictionary<string, string>
		{
			{ "en", "en-US" },
			{ "da", "da-DK" },
			{ "de", "de-DE" },
			{ "es", "es-ES" },
			{ "fr", "fr-FR" },
			{ "it", "it-IT" },
			{ "ja", "ja-JP" },
			{ "ko", "ko-KR" },
			{ "nl", "nl-NL" },
			{ "pl", "pl-PL" },
			{ "pt", "pt-BR" },
			{ "ru", "ru-RU" },
			{ "sv", "sv-SE" },
			{ "tr", "tr-TR" }
		};

/// <summary>
/// Gets the name of the culture with region
/// </summary>
/// <param name="languageCode">The language code.</param>
/// <returns>Full culture name</returns>
 string GetCultureName(string languageCode)
{
	string locale;
	var cultureName = CultureInfo.GetCultureInfo(languageCode).TextInfo.CultureName;
	if (LanguageCodeMap.TryGetValue(cultureName, out locale))
	{
		cultureName = locale;
	}

	return cultureName;
}
