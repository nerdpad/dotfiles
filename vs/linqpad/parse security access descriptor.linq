<Query Kind="Program" />

void Main()
{
	// var sddl = "O:BAG:BAD:(A;;RC;;;BA)";
	// var bad_sddl = "O:BAG:BAD";
	var sd = new System.Security.AccessControl.RawSecurityDescriptor(null);
	sd.GetSddlForm(System.Security.AccessControl.AccessControlSections.All).Dump();
}

// Define other methods and classes here