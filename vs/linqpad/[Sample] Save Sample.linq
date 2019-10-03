<Query Kind="Program">
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
	var policies = new List<Policy>
	{
		new Policy { UUID = Guid.Parse("5c66a4a3-5345-5be9-ae04-eb0779ad3e73"), ErrorCode = 5001 },
		new Policy { UUID = Guid.Parse("54e261f4-2d43-5a74-a0aa-466f438d0724"), ErrorCode = 5000 }
	};
	
	var samples = new List<Sample>
	{
		new Sample {BaselineID = 4133, BaselineStatusID = 4, Version = 3, ErrorCode = null, ErrorMessage = null, PolicyCount = 100, CompliantPolicyCount = 98, ComplianceLevel = 98, NonCompliantPolicies = policies }
	};

	using (var con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=AirWatchDev"))
	using (var cmd = new SqlCommand("interrogator.BaselineSample_Save", con) { CommandType = System.Data.CommandType.StoredProcedure })
	{
		var sampleList = this.GetBaselinePolicyTable(6, DateTime.Now, samples);
		var paramSamples = new SqlParameter("@BaselineSampleList", sampleList) { SqlDbType = SqlDbType.Structured };

		cmd.Parameters.Add(paramSamples);
		con.Open();
		cmd.ExecuteNonQuery();
	}
}

/// <summary>
/// Gets the baseline policy table.
/// </summary>
/// <param name="baseline">The baseline entity.</param>
/// <returns>A <see cref="DataTable"/> containing the baseline policies.</returns>
private DataTable GetBaselinePolicyTable(int deviceID, DateTime time, IList<Sample> samples)
{
	var table = new DataTable();
	table.Columns.Add("DeviceID", typeof(int));
	table.Columns.Add("SampleTime", typeof(DateTime));
	table.Columns.Add("TransmitTime", typeof(DateTime));
	table.Columns.Add("BaselineID", typeof(int));
	table.Columns.Add("BaselineStatusID", typeof(int));
	table.Columns.Add("Unchanged", typeof(bool));
	table.Columns.Add("Version", typeof(int));
	table.Columns.Add("ErrorCode", typeof(int));
	table.Columns.Add("ErrorMessage", typeof(string));
	table.Columns.Add("PolicyCount", typeof(int));
	table.Columns.Add("CompliantPolicyCount", typeof(int));
	table.Columns.Add("ComplianceLevel", typeof(int));
	table.Columns.Add("NonCompliantPolicies", typeof(byte[]));

	foreach (var policy in samples)
	{
		var content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(policy));
		table.Rows.Add(deviceID, time, time, policy.BaselineID, policy.BaselineStatusID, policy.Unchanged, policy.Version, policy.ErrorCode, policy.ErrorMessage, policy.PolicyCount, policy.CompliantPolicyCount, policy.ComplianceLevel, policy.NonCompliantPoliciesBytes);
	}

	return table;
}

// Define other methods and classes here
class Sample
{
	/// <summary>
	/// The non compliant policies
	/// </summary>
	private IList<Policy> nonCompliantPolicies;

	/// <summary>
	/// Gets or sets the baseline identifier.
	/// </summary>
	/// <value>
	/// The baseline identifier.
	/// </value>
	[JsonProperty("id")]
	public int BaselineID { get; set; }

	/// <summary>
	/// Gets or sets the baseline unique identifier.
	/// </summary>
	/// <value>
	/// The baseline unique identifier.
	/// </value>
	[JsonProperty("uuid")]
	public Guid BaselineUUID { get; set; }

	/// <summary>
	/// Gets or sets the baseline status identifier.
	/// </summary>
	/// <value>
	/// The baseline status identifier.
	/// </value>
	[JsonProperty("status")]
	public int BaselineStatusID { get; set; }

	/// <summary>
	/// Gets or sets the unchanged.
	/// </summary>
	/// <value>
	/// The unchanged.
	/// </value>
	[JsonProperty("unchanged")]
	public bool? Unchanged { get; set; }

	/// <summary>
	/// Gets or sets the version.
	/// </summary>
	/// <value>
	/// The version.
	/// </value>
	[JsonProperty("version")]
	public int? Version { get; set; }

	/// <summary>
	/// Gets or sets the error code.
	/// </summary>
	/// <value>
	/// The error code.
	/// </value>
	[JsonProperty("errorCode")]
	public int? ErrorCode { get; set; }

	/// <summary>
	/// Gets or sets the error message.
	/// </summary>
	/// <value>
	/// The error message.
	/// </value>
	[JsonProperty("errorMessage")]
	public string ErrorMessage { get; set; }

	/// <summary>
	/// Gets or sets the policy count.
	/// </summary>
	/// <value>
	/// The policy count.
	/// </value>
	[JsonProperty("policyCount")]
	public int? PolicyCount { get; set; }

	/// <summary>
	/// Gets or sets the compliant policy count.
	/// </summary>
	/// <value>
	/// The compliant policy count.
	/// </value>
	[JsonProperty("compliantPolicyCount")]
	public int? CompliantPolicyCount { get; set; }

	/// <summary>
	/// Gets or sets the compliance level.
	/// </summary>
	/// <value>
	/// The compliance level.
	/// </value>
	[JsonProperty("complianceLevel")]
	public int ComplianceLevel { get; set; }

	/// <summary>
	/// Gets or sets the non compliant policies.
	/// </summary>
	/// <value>
	/// The non compliant policies.
	/// </value>
	[JsonProperty("nonCompliantPolicies")]
	public IList<Policy> NonCompliantPolicies
	{
		get
		{
			return nonCompliantPolicies;
		}

		set
		{
			nonCompliantPolicies = value;
			NonCompliantPoliciesBytes = null;

			if (value != null)
			{
				var nonCompliancePoliciesSerialize = JsonConvert.SerializeObject(value);
				NonCompliantPoliciesBytes = Encoding.UTF8.GetBytes(nonCompliancePoliciesSerialize);
			}
		}
	}

	/// <summary>
	/// Gets or sets the non compliant policies bytes.
	/// </summary>
	/// <value>
	/// The non compliant policies bytes.
	/// </value>
	[JsonIgnore]
	public byte[] NonCompliantPoliciesBytes { get; set; }

}

class Policy
{
	[JsonProperty("uuid")]
	public Guid UUID { get; set; }

	[JsonProperty("errorCode")]
	public int? ErrorCode { get; set; }
}