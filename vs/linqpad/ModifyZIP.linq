<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.IO.Compression</Namespace>
</Query>

var path = @"c:\code\zip_test";
// var archiveName = "template.zip";
var archiveName = "template_with_metadata.zip";
var entryName = "registry.xml";
var metadataFileName = "metadata.json";
var bytes = File.ReadAllBytes(Path.Combine(path, archiveName));

// read the archive
using (var stream = new MemoryStream())
{
	stream.Write(bytes, 0, bytes.Length);

	using (var zip = new ZipArchive(stream, ZipArchiveMode.Update))
	{
		// find existing entry
		var entry = zip.GetEntry(metadataFileName);
		JArray oldContents = null;

		if (entry != null)
		{
			// merge metadata.json when it exists
			var newContents = JsonConvert.DeserializeObject<JArray>(File.ReadAllText(Path.Combine(path, metadataFileName)));
			
			using (var reader = new StreamReader(entry.Open()))
			{
				oldContents = JsonConvert.DeserializeObject<JArray>(reader.ReadToEnd()); 
			}
			
			foreach (var o in newContents)
			{
				oldContents.Add(o);
			}
		}
		
		entry?.Delete();

		// create a new entry
		entry = zip.CreateEntry(metadataFileName);
		using (var writer = new StreamWriter(entry.Open()))
		{
			writer.Write(JsonConvert.SerializeObject(oldContents));
		}
	}
	
	File.WriteAllBytes(Path.Combine(path, "combined.zip"), stream.ToArray());
}