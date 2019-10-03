<Query Kind="Program" />

void Main()
{
	var rootPath = @"C:\code\airw\uem_seed"; // portion of path which will be ignored from the output
	var paths = new string[] {
		@"C:\code\airw\uem_seed\SeedData\Catalogs\AdmX\1709",
		@"C:\code\airw\uem_seed\SeedData\Catalogs\AdmX\1803",
		@"C:\code\airw\uem_seed\SeedData\Catalogs\Json\1709"
	};

	var sourceLocale = "en-US";
	var targetLocales = new Dictionary<string, string>
	{
		{ "da-DK", "Danish" },
		{ "de-DE", "German" },
		{ "es-ES", "Spanish" },
		{ "fr-FR", "French" },
		{ "it-IT", "Italian" },
		{ "ja-JP", "Japanese" },
		{ "ko-KR", "Korean" },
		{ "nl-NL", "Dutch" },
		{ "pl-PL", "Polish" },
		{ "pt-BR", "Portuguese" },
		{ "ru-RU", "Russian" },
		{ "sv-SE", "Swedish" },
		{ "tr-TR", "Turkish" },
		{ "zh-CN", "zh-Hans" },
		{ "zh-TW", "zh-Hant" }
	};

	var extensionMap = new Dictionary<string, string>() { { ".admx", ".adml" }, { ".json", ".json" } };

	var missingTranslationFiles = new Dictionary<string, List<TranslationFile>>();

	foreach (var sourceDir in paths)
	{
		// get files in the source directory
		var policyFiles = Directory.GetFiles(sourceDir);

		foreach (var policyFile in policyFiles)
		{
			// for each of this policy source files, there should be a translation file available in the English (en-US) folder
			var translationFileName = Path.GetFileNameWithoutExtension(policyFile);
			var translationFileExtension = extensionMap[Path.GetExtension(policyFile).ToLower()];
			var translationFileNameWithExtension = $"{translationFileName}{translationFileExtension}";
			var sourcePolicyTranslationFile = Path.Combine(sourceDir, sourceLocale, translationFileNameWithExtension);
			var sourcePolicyTranslationFolder = Path.GetDirectoryName(sourcePolicyTranslationFile);

			// if the source translation file doesn't exist then we cannot continue with this file.
			if (!File.Exists(sourcePolicyTranslationFile))
			{
				Console.WriteLine($"Cannot find translation source: {sourcePolicyTranslationFile}");
				continue;
			}

			if (!missingTranslationFiles.ContainsKey(sourceDir))
			{
				missingTranslationFiles.Add(sourceDir, new List<TranslationFile>());
			}

			// find which are the missing language files for this policy file
			foreach (var targetLocale in targetLocales.Keys)
			{
				var targetPolicyTranslationFile = Path.Combine(sourceDir, targetLocale, $"{translationFileName}{translationFileExtension}");

				if (File.Exists(targetPolicyTranslationFile))
				{
					continue;
				}

				var translationFile = missingTranslationFiles[sourceDir].FirstOrDefault(tf => tf.SourceFile.Equals(translationFileNameWithExtension, StringComparison.OrdinalIgnoreCase));
				if (translationFile == null)
				{
					translationFile = new TranslationFile { SourceFile = translationFileNameWithExtension, SourceLocale = sourceLocale };
					missingTranslationFiles[sourceDir].Add(translationFile);
				}

				translationFile.MissingLocales.Add(targetLocale);
			}
		}
	}

	// missingTranslationFiles.Dump();

	var sb = new StringBuilder();

	foreach (var sourceTranslationPath in missingTranslationFiles.Keys)
	{
		// header
		var sourceLocation = string.Join("/", sourceTranslationPath.Replace(rootPath, string.Empty).Split(new char[] { '\\' }));
		sb.AppendLine($"[{sourceLocation}]");

		foreach (var translationFile in missingTranslationFiles[sourceTranslationPath])
		{
			var sourceFileName = Path.GetFileNameWithoutExtension(translationFile.SourceFile);
			var sourceFileExtension = Path.GetExtension(translationFile.SourceFile);
			var languages = $"\"{string.Join("\", \"", translationFile.MissingLocales.Select(cultureCode => targetLocales[cultureCode]))}\"";

			sb.AppendLine($"{translationFile.SourceLocale}/{translationFile.SourceFile} => %M/{sourceFileName}{sourceFileExtension} Languages: {languages}");
		}

		sb.AppendLine();
	}

	sb.ToString().Dump();

	File.WriteAllText(Path.Combine(rootPath, "temp_localization_targets.txt"), sb.ToString());
}

// Define other methods and classes here
class TranslationFile
{
	public TranslationFile()
	{
		this.MissingLocales = new List<string>();
	}

	public string SourceFile { get; set; }
	public string SourceLocale { get; set; }
	public List<string> MissingLocales { get; set; }
}