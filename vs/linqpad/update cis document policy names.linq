<Query Kind="Program">
  <Connection>
    <ID>a38235a2-4ac4-45c2-8c12-14f7d4486ce6</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <NoPluralization>true</NoPluralization>
    <Database>BaselineData</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var regex = new Regex(@"(?<=')(.*?)(?=')");
	// var testString = "(L1) Ensure 'Change the time zone' is set to 'Administrators, LOCAL SERVICE, Users'";

	using (var scope = new TransactionScope())
	{
		foreach (var row in this.CIS_1803_L2)
		{
			var policyName = this.Extract(regex, row.Title);
			if (string.IsNullOrWhiteSpace(policyName))
			{
				continue;
			}

			row.PolicyName = policyName;
			this.SubmitChanges();
		}
		
		scope.Complete();
	}
}

// Define other methods and classes here
string Extract(Regex regex, string source)
{
	return regex.Match(source)?.Value;
}