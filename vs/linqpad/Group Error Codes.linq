<Query Kind="Program" />

void Main()
{
	var result = new List<BaselineInstallStatusSummaryEntity>
	{
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Installed, Count = 1 },
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Pending, Count = 1 },
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Error, Count = 1, ErrorCode = 102 },
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Error, Count = 1, ErrorCode = 101 },
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Error, Count = 1, ErrorCode = 100 },
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Error, Count = 1, ErrorCode = 100 },
		new BaselineInstallStatusSummaryEntity { Status = BaselineStatus.Error, Count = 1, ErrorCode = 101 }
	};
	
	var errorResults = result.Where(o => o.ErrorCode != 0).ToList();
	
	if (errorResults.Count == 0)
	{
		result.Dump();
		return;
	}
	
	result.RemoveAll(o => o.ErrorCode != 0);
	
	var errorResultGrouped = from er in errorResults
		orderby er.Status ascending, er.ErrorCode ascending
		group er by new { er.Status } into erg
		select new BaselineInstallStatusSummaryEntity
		{
			Status = erg.Key.Status,
			Count = erg.Sum(e => e.Count),
			Reasons = (
				from ergi in erg
				group ergi by ergi.ErrorCode into ergig
				select new BaselineInstallStatusSummaryEntity
				{
					Status = erg.Key.Status,
					ErrorCode = ergig.Key,
					Count = ergig.Sum(e => e.Count)
				}
				).ToList()
		};
	
	result.AddRange(errorResultGrouped);
	
	result.OrderBy(o => o.Status).Dump();
}

// Define other methods and classes here
public enum BaselineStatus
{
	Installed,
	Pending,
	Error
}

public class BaselineInstallStatusSummaryEntity
{
	/// <summary>
	/// Gets or sets the baseline install status.
	/// </summary>
	/// <value>
	/// The status.
	/// </value>
	public BaselineStatus Status { get; set; }

	/// <summary>
	/// Gets or sets the error code.
	/// </summary>
	/// <value>
	/// The error code.
	/// </value>
	public int ErrorCode { get; set; }

	/// <summary>
	/// Gets or sets the count of devices with this install status.
	/// </summary>
	/// <value>
	/// The count.
	/// </value>
	public int Count { get; set; }

	/// <summary>
	/// Gets or sets the reasons.
	/// </summary>
	/// <value>
	/// The reasons.
	/// </value>
	public List<BaselineInstallStatusSummaryEntity> Reasons { get; set; }
}
