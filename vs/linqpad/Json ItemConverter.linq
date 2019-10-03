<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var str = "{\"items\": [\"val\", {\"name\": \"name1\", \"value\": \"val1\"}]}";
	var option = JsonConvert.DeserializeObject<Option>(str);
	option.Dump();
	
	str = JsonConvert.SerializeObject(option);
	str.Dump();
}

// Define other methods and classes here
public class Option
{
	[JsonProperty("items", ItemConverterType = typeof(StringToNameValueConverter))]
	public List<NameValue> Items { get; set; }
}
public class StringToNameValueConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		throw new NotImplementedException();
	}

	public override bool CanWrite => true;
	public override bool CanRead => true;

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		var token = JToken.Load(reader);
		
		// if reading a string item, then it must be just the value
		if (token.Type == JTokenType.String)
		{
			return new NameValue { Value = token.ToString() };
		}
		// if reading an object item, then it must be a name value type
		else if (token.Type == JTokenType.Object)
		{
			return token.ToObject(typeof(NameValue));
		}
		
		throw new Newtonsoft.Json.JsonSerializationException("The item is netither a string, or name value type");
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		// if the object that we are writing is already a string which is unlikely
		// but it doesn't need convertion
		if (value is NameValue nameValue && string.IsNullOrWhiteSpace(nameValue.Name))
		{
			writer.WriteValue(nameValue.Value);
			return;
		}
		
		serializer.Serialize(writer, value);
	}
}
public class NameValue
{
	[JsonProperty("name")]
	public string Name { get; set; }
	[JsonProperty("value")]
	public string Value { get; set; }
}