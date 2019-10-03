<Query Kind="Statements" />

var basePath = @"C:\code\airw\uem_seed\SeedData\Catalogs\AdmX";
var catalog = "1709";

// find all the ADMX files
var catalogPath = Path.Combine(basePath, catalog);

var admxFiles = Directory.GetFiles(catalogPath);
var languages = new List<string> { "tr-TR" }; //Directory.GetDirectories(catalogPath).Select(d => Path.GetFileName(d)).ToList();

foreach (var language in languages)
{
	var admlFilePath = Path.Combine(catalogPath, language, $"{Path.GetFileNameWithoutExtension(admxFileName)}.adml");
	if (File.Exists(admlFilePath))
	{
		// rename it
		admlFilePath.Dump();
		File.Move(admlFilePath, Path.Combine(catalogPath, language, $"{Path.GetFileNameWithoutExtension(admxFileName)}.adml"));
	}
}

foreach (var admxFile in admxFiles)
{
	// Rename the Admx File
	var admxFileName = Path.GetFileName(admxFile);
	File.Move(admxFile, Path.Combine(catalogPath, $"{Path.GetFileNameWithoutExtension(admxFileName)}.admx"));

	// Rename all the ADML files
}