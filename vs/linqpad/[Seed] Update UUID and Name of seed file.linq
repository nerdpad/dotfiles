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
	var osVersion1709 = 2;
	var osVersion1803 = 3;
	var osVersion1809 = 4;
	var osVersionID = osVersion1809;
	
	var path = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\seed\1809\RecommendedValues\WindowsServices.json";
	
	var contents = File.ReadAllText(path);
	
	var seed = JsonConvert.DeserializeObject<Seed>(contents);
	
	var policiesWithUUID = seed.Policies.Where(p => !p.UUID.Equals(Guid.Empty) && string.IsNullOrWhiteSpace(p.Name));
	
	// update Names
	foreach (var policy in seed.Policies)
	{
		var source = (from p in this.Policy
					join ax in this.ADMX on p.ADMXID equals ax.ID
					join os in this.OSVersion on ax.OSVersionID equals os.ID
					where os.ID == osVersionID && p.UUID.Equals(policy.UUID)
					select p).FirstOrDefault();
					
		if (source == null)
		{
			continue;
		}
		
		policy.Name = source.Name;
	}
	
	var policiesWithName = seed.Policies.Where(p => p.UUID.Equals(Guid.Empty) && !string.IsNullOrWhiteSpace(p.Name));
	
	foreach (var policy in seed.Policies)
	{
		var source = (from p in this.Policy
					join ax in this.ADMX on p.ADMXID equals ax.ID
					join os in this.OSVersion on ax.OSVersionID equals os.ID
					where os.ID == osVersionID && p.Name.Equals(policy.Name)
					select p).FirstOrDefault();
					
		if (source == null)
		{
			continue;
		}
		
		policy.UUID = source.UUID;
	}
	
	var pending = seed.Policies.Where(p => p.UUID.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(p.Name)).ToList();

	if (pending.Count > 0)
	{
		pending.Dump();
		throw new Exception("Cannot find all policies");
	}
	
	File.WriteAllText(
	path,
	JsonConvert.SerializeObject(
		seed,
		Newtonsoft.Json.Formatting.Indented,
		new JsonSerializerSettings { ContractResolver = new CustomContractResolver(new[] { "uuid" }) }));

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