<Query Kind="Statements" />

var basePath = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\seed\ADMX Catalog\";
var sourceADMXFileName = "MicrosoftEdge.admx";
var catalog = "1809";
var targetCatalog = "1903";

// find all the ADMX files
var catalogPath = Path.Combine(basePath, catalog);

var admxFiles = Directory.GetFiles(catalogPath);
var languages = Directory.GetDirectories(catalogPath).Select(d => Path.GetFileName(d)).ToList();

var list = new Dictionary<string, IList<string>>();

var admxFileName = Path.Combine(catalogPath, sourceADMXFileName);
	
// Rename all the ADML files
foreach (var language in languages)
{
	var admlFileName = $"{Path.GetFileNameWithoutExtension(admxFileName)}.adml";
	var admlFilePath = Path.Combine(catalogPath, language, admlFileName);
	
	if (File.Exists(admlFilePath)) {
		// copy to the target catalog
		var targetADMLFilePath = Path.Combine(basePath, targetCatalog, language, admlFileName);
		
		targetADMLFilePath.Dump();
		File.Copy(admlFilePath, targetADMLFilePath, true);
	}
}
