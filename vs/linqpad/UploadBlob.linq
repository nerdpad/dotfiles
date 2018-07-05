<Query Kind="Statements">
  <Connection>
    <ID>8b0d9bab-fea5-44d6-a2cd-206afb878aa0</ID>
    <Persist>true</Persist>
    <Server>172.16.85.2</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>AirWatchAdmin_AirWatch</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAkhzbKwJ07kKFk7cIdPdPiQAAAAACAAAAAAADZgAAwAAAABAAAAAw4VP3a2JsROgQzMra205IAAAAAASAAACgAAAAEAAAAFS5Frgeo5iUwDUle2poJXIQAAAAhT7vyjPGgMNMJNDyqQPkNhQAAAAsyS1MYFsTaRj/DcdPf3VXoW1oxg==</Password>
    <Database>winbaselines</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>Microsoft.Practices.EnterpriseLibrary.Data.dll</NuGetReference>
  <Namespace>Microsoft.Practices.EnterpriseLibrary.Data</Namespace>
</Query>

var path = @"c:\code\airw\baselines\";

var files = new Dictionary<int, string>();
files.Add(471, "1709_MSFT_Win10_Baseline.zip");
files.Add(472, "1709_CIS_Win10_L1_Baseline.zip");
files.Add(474, "1709_CIS_Win10_L2_Baseline.zip");
files.Add(473, "1709_CIS_Win10_L1_Bitlocker_Baseline.zip");
files.Add(475, "1709_CIS_Win10_L2_Bitlocker_Baseline.zip");

var query = "UPDATE dbo.BlobMaster SET BlobStream = @BlobStream, BlobType = 'application/zip' WHERE BlobID = @BlobID";

this.Connection.Open();
foreach (var file in files)
{
	using (var cmd = new SqlCommand(query, this.Connection as SqlConnection))
	{

		var streamParam = new SqlParameter();
		streamParam.ParameterName = "@BlobStream";
		streamParam.DbType = DbType.Binary;
		streamParam.Direction = ParameterDirection.Input;
		streamParam.Value = ReadBytes(file.Key);

		cmd.Parameters.Add(streamParam);
		var idParam = new SqlParameter();
		idParam.ParameterName = "@BlobID";
		idParam.DbType = DbType.Int32;
		idParam.Direction = ParameterDirection.Input;
		idParam.Value = file.Key;
		cmd.Parameters.Add(idParam);

		cmd.ExecuteNonQuery();
	}
}

this.Connection.Close();

byte[] ReadBytes(int id)
{
	return File.ReadAllBytes(Path.Combine(path, files[id]));
}