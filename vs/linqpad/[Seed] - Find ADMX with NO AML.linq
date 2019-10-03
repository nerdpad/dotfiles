<Query Kind="Statements" />

var basePath = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\seed\ADMX Catalog\";
var catalog = "1809";

// find all the ADMX files
var catalogPath = Path.Combine(basePath, catalog);

var admxFiles = Directory.GetFiles(catalogPath);
var languages = Directory.GetDirectories(catalogPath).Select(d => Path.GetFileName(d)).ToList();

var list = new Dictionary<string, IList<string>>();

foreach (var admxFile in admxFiles)
{
	var admxFileName = Path.GetFileName(admxFile);
	list[admxFileName] = new List<string>();
	
	// Rename all the ADML files
	foreach (var language in languages)
	{
		var admlFilePath = Path.Combine(catalogPath, language, $"{Path.GetFileNameWithoutExtension(admxFileName)}.adml");
		if (File.Exists(admlFilePath)) {
			list[admxFileName].Add(language);
		}
	}
}

list["MicrosoftEdge.admx"].Dump();

list.Where(o => o.Value.Count == 0).Select(o => o.Key).Dump("With No ADML Files");

list.Where(o => o.Value.Count != 0 && !o.Value.Contains("en-US")).Select(o => o.Key).Dump("With No English ADML Files");