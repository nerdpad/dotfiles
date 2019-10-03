<Query Kind="Program">
  <Namespace>System.Security.Principal</Namespace>
</Query>

void Main()
{
	var names = new string[] {
		"Guest",
		"NT AUTHORITY\\SERVICE",
		"BUILTIN\\Administrators",
		"BUILTIN\\Backup Operators",
		"Everyone",
		"BUILTIN\\Users",
		"BUILTIN\\Remote Desktop Users",
		"NT AUTHORITY\\NETWORK SERVICE",
		"NT AUTHORITY\\LOCAL SERVICE",
		"BUILTIN\\Power Users",
		"BUILTIN\\Remote Desktop Users",
		"NT AUTHORITY\\Service",
		"BUILTIN\\Guests",
		"Local Account",
		"Administrator",
		// "BUILTIN\\Print Operators",
		"BUILTIN\\Power Users",
		"NT SERVICE\\WdiServiceHost",
		// "BUILTIN\\Server Operators",
		// "BUILTIN\\Replicators"
	};

	var list = new List<Account>();
	
	foreach (var name in names)
	{
		try
		{
			var sid = (SecurityIdentifier)new NTAccount(name).Translate(typeof(SecurityIdentifier));
			list.Add(new Account { Name = name, Sid = sid.Value });
		}
		catch (Exception ex)
		{
			throw new Exception($"{name}: {ex.Message}");
		}
	}
	
	list.Dump();
}

// Define other methods and classes here
class Account
{
	public string Name { get; set; }
	public string Sid { get; set; }
}