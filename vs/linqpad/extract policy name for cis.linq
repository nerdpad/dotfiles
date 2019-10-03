<Query Kind="Program" />

void Main()
{
	var regex = new Regex(@"(?<=')(.*?)(?=')");
	var testString = "(L1) Ensure 'Change the time zone' is set to 'Administrators, LOCAL SERVICE, Users'";
	
	var policyName = this.Extract(regex, testString);
	policyName.Dump(); // out to console
}

// Define other methods and classes here
string Extract(Regex regex, string source)
{
	return regex.Match(source)?.Value;
}