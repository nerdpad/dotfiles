<Query Kind="Statements">
  <NuGetReference>Google.Cloud.Translation.V2</NuGetReference>
</Query>

var basePath = @"c:\code\admx";
var admlFileName = "baselines_root_categories.adml";
var sourceADMLFile = Path.Combine(basePath, "en-US", admlFileName);
var languages = new List<string> { "da-DK", "de-DE", "es-ES", "fr-FR", "it-IT", "ja-JP", "ko-KR", "nl-NL", "pl-PL", "pt-BR", "ru-RU", "sv-SE", "tr-TR", "zh-CN", "zh-TW" };

foreach (var language in languages)
{
	// Create the file for the language
	var targetFile = Path.Combine(basePath, language, admlFileName);
	File.Copy(sourceADMLFile, targetFile, true);

	// load the file to translate
	var doc = new XmlDocument();
	doc.Load(targetFile);

}