<Query Kind="Statements">
  <Connection>
    <ID>a02bf2bb-7640-4a20-b569-9acf2d717a8f</ID>
    <Persist>true</Persist>
    <Server>172.16.65.46</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAuFxAQ1rn2ke7el4Qi4ACZAAAAAACAAAAAAADZgAAwAAAABAAAAB2dtW/H23LOyV5HRA3RxpqAAAAAASAAACgAAAAEAAAAEsgx+JiCbVEcvXQO3kEO7kQAAAAUeGXC3D81MATaRJTa91/gBQAAAD2gXtxdJHh3WflO1go/ywIZSuSLQ==</Password>
    <Database>autowinrel02</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var blobID = 735;
var blob = BlobMasters.FirstOrDefault(bm => bm.BlobID == blobID);
var output = @"c:\temp\test.gif";

File.WriteAllBytes(output, blob.BlobStream.ToArray());