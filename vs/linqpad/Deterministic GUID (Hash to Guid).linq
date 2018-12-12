<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	for (var i = 0; i < 3; i++) {
		ConvertToMd5HashGUID("Test_1").Dump();
	}
	
	ConvertToMd5HashGUID("Test_2").Dump();
}

// Define other methods and classes here
/// <summary>
/// Convert string to Guid
/// </summary>
/// <param name="value">the string value</param>
/// <returns>the Guid value</returns>
public static Guid ConvertToMd5HashGUID(string value)
{
	// convert null to empty string - null can not be hashed
	if (value == null)
		value = string.Empty;

	// get the byte representation
	var bytes = Encoding.Default.GetBytes(value);

	// create the md5 hash
	MD5 md5Hasher = MD5.Create();
	byte[] data = md5Hasher.ComputeHash(bytes);

	// convert the hash to a Guid
	return new Guid(data);
}