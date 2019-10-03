<Query Kind="Program">
  <Connection>
    <ID>a38235a2-4ac4-45c2-8c12-14f7d4486ce6</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <NoPluralization>true</NoPluralization>
    <Database>BaselineDB</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var path = @"C:\code\policy_catalog";

	var versions = Directory.GetDirectories(path);
	var languages = new List<string>();
	foreach(var version in versions)
	{
		languages.AddRange(Directory.GetDirectories(version).Select(d => Path.GetFileName(d)));
	}
	
	languages = languages.OrderBy(l => l).Distinct().ToList();
	
	var missingLanguages = languages.Where(l => !this.Locale.Any(lo => lo.Culture.Equals(l))).ToList();
	
	missingLanguages.Dump();
	
	var id = 17;
	var codes = new List<string>();
	foreach (var code in missingLanguages)
	{
		var culture = CultureInfo.GetCultureInfo(code);
		codes.Add($"new Locale {{ ID = {id++}, UUID = Guid.Parse(\"{Guid.NewGuid()}\"), Culture = \"{culture.TextInfo.CultureName}\", Language = \"{culture.DisplayName}\" }}");
	}
	
	string.Join(",\n", codes).Dump();
}

// Define other methods and classes here
