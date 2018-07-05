<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.CodeDom.Compiler</Namespace>
  <Namespace>System.CodeDom</Namespace>
</Query>

void Main()
{
	var obj = new
	{
		name = "Mondecorp Global Security Baseline",
		description = "Baseline for global security",
		rootLocationGroupID = 7,
		baselineVendorTemplateID = 2,
		osVersionID = 2,
		securityLevelID = 2
	};
	
	ToLiteral(JsonConvert.SerializeObject(obj)).Dump();
}

// Define other methods and classes here
private static string ToLiteral(string input)
{
	using (var writer = new StringWriter())
	{
		using (var provider = CodeDomProvider.CreateProvider("CSharp"))
		{
			provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
			return writer.ToString();
		}
	}
}