<Query Kind="Statements">
  <Connection>
    <ID>be136497-e374-4558-bfcf-431d2398b513</ID>
    <Persist>true</Persist>
    <Server>172.16.84.125</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAdmux5f4kqkKJep0X8D+DowAAAAACAAAAAAADZgAAwAAAABAAAABiDg4tyY15iOEw0Yn/N9NMAAAAAASAAACgAAAAEAAAAG8S5pES0Mf8kaDVNtp58mgQAAAAcX5SljjnhxDCFQqm4mmuoxQAAABDDpSsFIZxQITySb8l4tkOP/oxyw==</Password>
    <Database>oemdell</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var blobID = 448;
var blob = BlobMasters.FirstOrDefault(bm => bm.BlobID == blobID);
var output = @"c:\temp\test.gif";

File.WriteAllBytes(output, blob.BlobStream.ToArray());