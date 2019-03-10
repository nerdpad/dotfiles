<Query Kind="Statements" />

var basePath = @"C:\code\catalog";
var catalog = "1809";

// find all the ADMX files
var catalogPath = Path.Combine(basePath, catalog);

var admxFiles = Directory.GetFiles(catalogPath);
var languages = Directory.GetDirectories(catalogPath).Select(d => Path.GetFileName(d)).ToList();

foreach (var admxFile in admxFiles)
{
	// Rename the Admx File
	var admxFileName = Path.GetFileName(admxFile);
	File.Move(admxFile, Path.Combine(catalogPath, $"{Path.GetFileNameWithoutExtension(admxFileName)}.admx"));
	
	// Rename all the ADML files
	foreach (var language in languages)
	{
		var admlFilePath = Path.Combine(catalogPath, language, $"{Path.GetFileNameWithoutExtension(admxFileName)}.adml");
		if (File.Exists(admlFilePath)) {
			// rename it
			File.Move(admlFilePath, Path.Combine(catalogPath, language, $"{Path.GetFileNameWithoutExtension(admxFileName)}.adml"));
		}
	}
}