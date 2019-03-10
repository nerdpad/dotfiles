<Query Kind="Statements" />

var path = @"C:\code\airw\uem\AW.Windows.UEM.Data\SeedData\Catalogs\AdmX";
var catalogs = new List<string> { "1703", "1709", "1803", "1809" };
var file = "baselines_root_categories.adml";

foreach (var c in catalogs)
{
	var directories = Directory.GetDirectories(Path.Combine(path, c));
	foreach (var dir in directories)
	{
		var lang = Path.GetFileNameWithoutExtension(dir);
		
		if (lang.Equals("en-us"))
		{
			continue;
		}
		
		var source = Path.Combine(path, c, "en-us", file);
		var target = Path.Combine(path, c, lang, file);

		File.Copy(source, target, true);
	}
}
