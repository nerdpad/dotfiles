<Query Kind="Statements">
  <Connection>
    <ID>661cdcbe-51cd-4ec8-a09d-836efcca3fc2</ID>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPath>C:\code\airw\uem\AW.Windows.UEM.Data\bin\Debug\netstandard2.0\AW.Windows.UEM.Data.dll</CustomAssemblyPath>
    <CustomTypeName>AW.Windows.UEM.Data.UEMContext</CustomTypeName>
    <CustomCxString>Data Source=localhost;Initial Catalog=BaselineDB;User ID=AirWatchAdmin;Password=AirWatchAdmin;</CustomCxString>
  </Connection>
</Query>

var localeID = 1;
var uuid = Guid.Parse("a914cc3d-3907-5ccb-a976-8811fa3c3284");

var query = from p in Policy
			join ax in ADMX on p.ADMXID equals ax.ID
			join al in ADML on new { ADMXID = ax.ID, LocaleID = localeID } equals new { al.ADMXID, al.LocaleID }
			join dps in StringResource on new { Key = p.DisplayNameKey, ADMLID = al.ID } equals new { dps.Key, dps.ADMLID } into ds
			from displayString in ds.DefaultIfEmpty()
			join eps in StringResource on new { Key = p.ExplainTextKey, ADMLID = al.ID } equals new { eps.Key, eps.ADMLID } into es
			from explainString in es.DefaultIfEmpty()
			join pp in PolicyPresentation on new { Key = p.PresentationKey, ADMLID = al.ID } equals new { pp.Key, pp.ADMLID } into ppg
			from presentation in ppg.DefaultIfEmpty()
			where p.UUID.Equals(uuid)
			select new { Policy = p, DisplayString = displayString, ExplainString = explainString, Presentation = presentation };