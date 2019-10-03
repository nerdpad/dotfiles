<Query Kind="Program">
  <NuGetReference>DocumentFormat.OpenXml</NuGetReference>
  <Namespace>DocumentFormat.OpenXml.Packaging</Namespace>
  <Namespace>DocumentFormat.OpenXml.Spreadsheet</Namespace>
</Query>

void Main()
{
	var path = @"C:\Users\zzakaria\OneDrive - VMware, Inc\baselines\ms 1809\Ready To Seed\Computer.xlsx";
	var output = @"c:\temp";

	var uuids = this.ReadUUID(path);
	$"Total UUID Count: {uuids.Count}".Dump();
	
	List<Guid> batch = null;
	
	var skip = 0;
	var take = 10;
	
	var sb = new StringBuilder();
	
	while((batch = uuids.Skip(skip).Take(take).ToList()).Count != 0)
	{
		sb.AppendFormat("{0},\n\n", string.Join(", ", batch.Select(u => u.ToString())));
		
		sb.AppendLine();
		sb.AppendLine();
		
		skip += take;
	}
	
	File.WriteAllText(Path.Combine(output, "uuids.txt"), sb.ToString());
}

// Define other methods and classes here
private IList<Guid> ReadUUID(string path)
{
	var result = new List<Guid>();
	
	using (var spreadsheetDocument = SpreadsheetDocument.Open(path, false))
	{
		var workbookPart = spreadsheetDocument.WorkbookPart;
		
		var worksheetPart = workbookPart.WorksheetParts.First();
		
		var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
		
		var headerRead = false;
		foreach (var row in sheetData.Elements<Row>())
		{
			if (!headerRead)
			{
				headerRead = true;
				continue;
			}
			
			var uuidString = this.GetCellValue(spreadsheetDocument, row.Elements<Cell>().First());
			
			var uuid = Guid.Parse(uuidString);
			
			if (!uuid.Equals(Guid.Empty))
			{
				result.Add(uuid);
			}
		}
	}
	
	return result;
}

private string GetCellValue(SpreadsheetDocument doc, Cell cell)
{
	string value = cell.CellValue.InnerText;
	if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
	{
		return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
	}
	
	return value;
}