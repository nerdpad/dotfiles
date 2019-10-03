<Query Kind="Program">
  <Connection>
    <ID>a38235a2-4ac4-45c2-8c12-14f7d4486ce6</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <NoPluralization>true</NoPluralization>
    <Database>BaselineDB</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	var path = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\seed\1709";
	
	var files = Directory.GetFiles(path, "*.json");
	
	var categories = new List<string>();
	
	foreach (var file in files)
	{
		var data = JsonConvert.DeserializeObject<Seed>(File.ReadAllText(file));
		categories.AddRange(data.Policies.Select(p => $"VMware.Policies.RootCategories:{p.CategoryRefID}"));
	}
	
	categories = categories.Distinct().ToList();
	
	categories.Dump();
	
	var found = this.Category.Where(c => categories.Contains(c.Name)).Select(c => c.Name).Distinct().ToList();
	
	var missing = categories.Where(c => !found.Any(f => f == c)).ToList();
	
	missing.Dump();
}

// Define other methods and classes here
class Seed
{
	[JsonProperty("policies")]
	public List<Policy> Policies { get; set; }
}
class Master
{
	[JsonProperty("policy")]
	public Policy Policy { get; set; }
}
class Policy
{
	[JsonProperty("categoryRefID")]
	public string CategoryRefID { get; set; }
}