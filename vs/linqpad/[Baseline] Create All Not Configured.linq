<Query Kind="Program">
  <Connection>
    <ID>5cf7bc21-4c82-40fd-a3bb-9c1a76fa35af</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <Database>BaselineDB</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference Relative="..\..\..\airw\uem\AW.Windows.UEM.Services\bin\Debug\netstandard2.0\AW.Windows.UEM.Common.dll">C:\code\airw\uem\AW.Windows.UEM.Services\bin\Debug\netstandard2.0\AW.Windows.UEM.Common.dll</Reference>
  <Reference Relative="..\..\..\airw\uem\AW.Windows.UEM.Services\bin\Debug\netstandard2.0\AW.Windows.UEM.Services.dll">C:\code\airw\uem\AW.Windows.UEM.Services\bin\Debug\netstandard2.0\AW.Windows.UEM.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>AW.Windows.UEM.Services.Models</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
	var templateID = 15;
	
	var policies = (from bt in BaselineTemplates
		join btp in BaselineTemplatePolicies on bt.ID equals btp.BaselineTemplateID
		join p in Policies on btp.PolicyID equals p.ID
					where bt.ID == templateID
					select new PolicyItem { UUID = p.UUID }).ToList();

	var serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
	var json = JsonConvert.SerializeObject(policies, Newtonsoft.Json.Formatting.Indented, serializerSettings);
	json.Dump();
	Clipboard.SetText(json);
}

// Define other methods and classes here
