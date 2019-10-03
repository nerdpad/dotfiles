<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
</Query>

void Main()
{
	var list = new List<Test>();
	
	for (var i = 0; i < 10000; i++)
	{
		list.Add(new Test { Name = $"flasdjflasdjflasdjf{i}" });
	}

	byte[] data = null;
	var formatter = new BinaryFormatter();
	using (var stream = new MemoryStream())
	{
		formatter.Serialize(stream, list);
		data = stream.ToArray();
	}

	data.Length.Dump("Binary");
	
//	using (var stream = new MemoryStream(data))
//	{
//		formatter.Binder = new TestTypeConverter();
//
//		var newList = formatter.Deserialize(stream) as List<Test1>;
//		newList.Dump();
//	}
	
	
	data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list));
	
	data.Length.Dump("Json");
	
	JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), typeof(List<Test1>)).Dump();
}

// Define other methods and classes here
[Serializable]
class Test
{
	public string Name { get; set; }
}

[Serializable]
class Test1
{
	public string Name { get; set; }
}

class TestTypeConverter : SerializationBinder
{
	public override Type BindToType(string assemblyName, string typeName)
	{
		if (typeName.EndsWith("Test"))
		{
			return typeof (Test1);
		}
		
		return typeof (List<Test1>);
	}
}