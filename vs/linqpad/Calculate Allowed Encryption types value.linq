<Query Kind="Program" />

void Main()
{
	// calculate SupportedEncryptionTypes value combination
	var values = (EncTypes.AES128_HMAC_SHA1 | EncTypes.AES256_HMAC_SHA1 | EncTypes.FUTURE_ENC_TYPES);
	var numbers = new List<int?> { 8, 16, 2147483616 };
	((int)values).Dump();
	
	int? value = null;
	
	foreach (var num in numbers)
	{
		if (value == null)
		{
			value = num;
			continue;
		}
		
		value |= num;
	}
	
	value.Dump();
}

// Define other methods and classes here
[Flags]
enum EncTypes : int
{
	DES_CBC_CRC = 1,
	DES_CBC_MD5 = 2,
	RC4_HMAC_MD5 = 4,
	AES128_HMAC_SHA1 = 8,
	AES256_HMAC_SHA1 = 16,
	FUTURE_ENC_TYPES = 2147483616
}