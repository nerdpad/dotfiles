<Query Kind="Statements">
  <NuGetReference>VMware.UEM.AdmX</NuGetReference>
  <Namespace>Immidio.FlexProfiles.ManagementConsole.Admx</Namespace>
</Query>

var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><userEnvironmentSettings><setting type=\"Gpo\" name=\"registry_machine.xml\" tag=\"\" p=\"Gl9Hfuu45apoWEyxmlfJHRq9u6TshhXc3NHzd1q4e2k6jIbLceRQRyLepB9xrIxu7aKZbrDDLXqVJUND/6eErBv3Ni1C52Bq8OtDmwFqOt+Y1gUIJ/8KNCM9P4X7Cze1sqZ+JA7A9AYPgWtP9bC0s+oUWiDdwzUYaugaz+vMMYrxHYL6tFZH7zCkbucAFCKTz4eOoPTnKNAhfXfpP+Zl8GYEA28l0tHi72ScHQRVEioIXyo2KTAxg4ndlERBbr5Gt0UdR/CVWPmZoIsL6/3BRjmtm5qmimIomBSYMOAmw4+TN/eS8wTuWcBa0H1gYeR5X9LaIaxBC+AfKTiTTtfJ1b0CQ9VoOWg5ZKQ+j7aoupQxA57q5Ni5nOV/w48gstrsZvEwUsoSI+86Hk3T+VC1krb9o8c1bkloe4cMpddhZPoJCqdVNolDEgW0u65grmGzguia0lPBGoGnx2rEIyW9TMLUCGWrxEuTMvgh3fURFkVNGGg=\"><Categories /><Settings><Setting><AdmX>494</AdmX><CategoryName>925</CategoryName><CategoryNameStr>925</CategoryNameStr><PathToRootCategory>925</PathToRootCategory><PolicyName>AbsoluteMaxCacheSize</PolicyName><PolicyNameStr>Absolute Max Cache Size (in GB)</PolicyNameStr><PolicyElement><Element><ID>AbsoluteMaxCacheSize</ID><Enabled>False</Enabled><Actions /></Element></PolicyElement><Status>Enabled</Status><Elements><Element><ID>AbsoluteMaxCacheSize</ID><Enabled>True</Enabled><Actions><Action><Action>AddValue</Action><ValueType>DWord</ValueType><Key>HKLM*\\SOFTWARE\\Policies\\Microsoft\\Windows\\DeliveryOptimization</Key><ValueName>DOAbsoluteMaxCacheSize</ValueName><ValueData>10</ValueData></Action></Actions></Element></Elements></Setting></Settings></setting></userEnvironmentSettings>";
var doc = new XmlDocument();
doc.LoadXml(xml);

var settingElement = doc.SelectSingleNode("/userEnvironmentSettings/setting") as XmlElement;
var uncompressedDoc = Compression.GetUncompressedDocument(settingElement, "blah");
settingElement.InnerXml = doc.SelectSingleNode("/userEnvironmentSettings/setting").InnerXml;
settingElement.RemoveAttribute("p");
var decompressedXml = doc.OuterXml;

// format
string formattedXml = null;

using (var stream = new MemoryStream())
using (var writer = new XmlTextWriter(stream, Encoding.Unicode) { Formatting = Formatting.Indented })
{
	doc.WriteContentTo(writer);
	writer.Flush();
	stream.Flush();

	stream.Position = 0;

	using (var reader = new StreamReader(stream))
	{
		formattedXml = reader.ReadToEnd();
	}
}

formattedXml.Dump();



