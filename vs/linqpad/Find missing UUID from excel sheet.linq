<Query Kind="Program">
  <NuGetReference>DocumentFormat.OpenXml</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>DocumentFormat.OpenXml.Packaging</Namespace>
  <Namespace>DocumentFormat.OpenXml.Spreadsheet</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	var path = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\ms 1809\Ready To Seed\Computer.xlsx";
	var pathToUUIDFile = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\ms 1809\Ready To Seed\Computer.txt";
	var pathToSeedFile = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\seed\1809\RecommendedValues\LocalSecurityPolicies.json";
	
	IList<Dup> dupUUIDs;
	
	var uuids = this.ReadUUID(path);
	$"Excel Sheet UUID Count: {uuids.Count}".Dump();
	
	dupUUIDs = this.FindDuplicates(uuids);
	$"Duplicate UUID in Excel Count: {dupUUIDs.Count}".Dump();
	dupUUIDs.Dump();
	
	var fileUUIDs = this.ReadUUIDFiledUUIDs(pathToUUIDFile);
	$"UUID File UUID Count: {fileUUIDs.Count}".Dump();
	
	dupUUIDs = this.FindDuplicates(fileUUIDs);
	$"Duplicate UUID in UUID File Count: {dupUUIDs.Count}".Dump();
	dupUUIDs.Dump();
	
	var seedUUIDs = this.ReadSeedFileUUIDs(pathToSeedFile);
	$"Seed File UUID Count: {seedUUIDs.Count}".Dump();
	
	dupUUIDs = this.FindDuplicates(seedUUIDs);
	$"Duplicate UUID in Seed File Count: {dupUUIDs.Count}".Dump();
	dupUUIDs.Dump();
	
	var missingFromUUIDFile = uuids.Where(uuid => !fileUUIDs.Contains(uuid)).ToList();
	"Missing from UUID File".Dump();
	missingFromUUIDFile.Dump();
	
	var missingFromSeedFile = uuids.Where(uuid => !seedUUIDs.Contains(uuid)).ToList();
	"Missing from Seed File".Dump();
	missingFromSeedFile.Dump();
}

// Define other methods and classes here
private IList<Dup> FindDuplicates(IList<Guid> uuids)
{
	return uuids.GroupBy(u => u).Where(g => g.Count() > 1).Select(g => new Dup { UUID = g.Key, Count = g.Count() }).ToList();
}

private IList<Guid> ReadSeedFileUUIDs(string path)
{
	var data = JsonConvert.DeserializeObject<Seed>(File.ReadAllText(path));
	
	return data.Policies.Select(p => p.UUID).ToList();
}

private IList<Guid> ReadUUIDFiledUUIDs(string path)
{
	var seedContents = File.ReadAllText(path);
	var splits = seedContents.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

	var result = new List<Guid>();
	foreach (var str in splits)
	{
		Guid parsed = Guid.Empty;
		Guid.TryParse(str, out parsed);
		
		if (parsed.Equals(Guid.Empty))
		{
			continue;
		}
		
		result.Add(parsed);
	}
	
	return result;
}

private IList<Guid> ReadUUID(string path)
{
	var result = new List<Guid>();
	
	using (var spreadsheetDocument = SpreadsheetDocument.Open(path, false))
	{
		var workbookPart = spreadsheetDocument.WorkbookPart;
		
		var worksheetPart = workbookPart.WorksheetParts.First();
		
		var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
		
		var headerRead = false;
		foreach (var row in sheetData.Elements<Row>())
		{
			if (!headerRead)
			{
				headerRead = true;
				continue;
			}
			
			var uuidString = this.GetCellValue(spreadsheetDocument, row.Elements<Cell>().First());
			
			var uuid = Guid.Parse(uuidString);
			
			if (!uuid.Equals(Guid.Empty))
			{
				result.Add(uuid);
			}
		}
	}
	
	return result;
}

private string GetCellValue(SpreadsheetDocument doc, Cell cell)
{
	string value = cell.CellValue.InnerText;
	if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
	{
		return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
	}
	
	return value;
}

class Dup
{
	public Guid UUID { get; set; }
	public int Count { get; set; }
}
class Seed
{
	[JsonProperty("policies")]
	public List<PolicyItem> Policies { get; set; }
}
public enum PolicyState : int
{
	/// <summary>
	/// The policy is not configured
	/// </summary>
	NotConfigured = 0,

	/// <summary>
	/// The policy is enabled
	/// </summary>
	Enabled = 1,

	/// <summary>
	/// The policy is disabled
	/// </summary>
	Disabled = 2
}

public class PolicyItem
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PolicyItem"/> class.
	/// </summary>
	public PolicyItem()
	{
		this.Options = new List<PolicyOption>();
	}

	/// <summary>
	/// Gets or sets the unique identifier.
	/// </summary>
	/// <value>
	/// The unique identifier.
	/// </value>
	[JsonProperty("uuid")]
	public Guid UUID { get; set; }

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	[JsonProperty("name")]
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the status.
	/// </summary>
	/// <value>
	/// The status.
	/// </value>
	[JsonProperty("status")]
	[JsonConverter(typeof(StringEnumConverter))]
	public PolicyState Status { get; set; }

	/// <summary>
	/// Gets or sets the options.
	/// </summary>
	/// <value>
	/// The options.
	/// </value>
	[JsonProperty("options")]
	public List<PolicyOption> Options { get; set; }
}

// Define other methods and classes here
public class PolicyOption
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	[JsonProperty("id")]
	public string ID { get; set; }

	/// <summary>
	/// Gets or sets the value.
	/// </summary>
	/// <value>
	/// The value.
	/// </value>
	[JsonProperty("value")]
	public object Value { get; set; }
}
public class CustomContractResolver : DefaultContractResolver
{
	/// <summary>
	/// The properties
	/// </summary>
	private readonly IList<string> propertiesToIgnore;

	/// <summary>
	/// Initializes a new instance of the <see cref="CustomContractResolver" /> class.
	/// </summary>
	/// <param name="propertiesToIgnore">The properties to ignore.</param>
	public CustomContractResolver(string[] propertiesToIgnore)
	{
		this.propertiesToIgnore = propertiesToIgnore;
	}

	/// <summary>
	/// Creates properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.
	/// </summary>
	/// <param name="type">The type to create properties for.</param>
	/// <param name="memberSerialization">The member serialization mode for the type.</param>
	/// <returns>
	/// Properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.
	/// </returns>
	protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
	{
		var modelProperties = base.CreateProperties(type, memberSerialization);

		return modelProperties.Where(p => !this.propertiesToIgnore.Contains(p.PropertyName)).ToList();
	}
}