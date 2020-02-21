<Query Kind="Program">
  <NuGetReference>JsonSubTypes</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>JsonSubTypes</Namespace>
</Query>

void Main()
{
	var json = @"{ ""type"": ""Decimal"", ""valueChanger"": {""type"": ""LowerLimit"", ""value"": -1 }}";
	var obj = JsonConvert.DeserializeObject<Element>(json);
	obj.Dump();
}

// Define other methods and classes here
enum ValueType
{
	None = 0,
	LowerLimit = 1
}

[JsonConverter(typeof(JsonSubtypes), "type")]
[JsonSubtypes.KnownSubType(typeof(LowerLimitValueChanger), ValueType.LowerLimit)]
abstract class ValueChanger
{
	[JsonProperty("type")]
	[JsonConverter(typeof(StringEnumConverter))]
	public ValueType Type { get; set; }
	
	protected ValueChanger(ValueType type)
	{
		this.Type = type;
	}
}

abstract class ValueChanger<T> : ValueChanger
{
	[JsonProperty("value")]
	public T Value { get; set; }

	protected ValueChanger(ValueType type)
	: base(type)
	{
	}
}

class LowerLimitValueChanger : ValueChanger<int>
{
	public LowerLimitValueChanger()
	: base(ValueType.LowerLimit)
	{
	}
}

enum ElementType
{
	None = 0,
	Decimal = 1
}

[JsonConverter(typeof(JsonSubtypes), "type")]
[JsonSubtypes.KnownSubType(typeof(DecimalElement), ElementType.Decimal)]
abstract class Element
{
	[JsonProperty("type")]
	[JsonConverter(typeof(StringEnumConverter))]
	public ElementType Type { get; set; }
	
	protected Element(ElementType type)
	{
		this.Type = type;
	}
}

class DecimalElement : Element
{
	public DecimalElement()
	: base(ElementType.Decimal)
	{
	}
	
	[JsonProperty("valueChanger")]
	public ValueChanger ValueChanger { get; set; }
}

class Test
{
	[JsonProperty("valueChanger")]
	public ValueChanger ValueChanger { get; set; }
}