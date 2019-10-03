<Query Kind="SQL">
  <Connection>
    <ID>a38235a2-4ac4-45c2-8c12-14f7d4486ce6</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <NoPluralization>true</NoPluralization>
    <Database>AirWatchDev</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

declare @b varbinary(max)
	,@id int = 2563
select @b = blobStream from dbo.BlobMaster where blobID = @id

select cast(@b as varchar(max))