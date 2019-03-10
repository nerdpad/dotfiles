<Query Kind="Expression">
  <Connection>
    <ID>661cdcbe-51cd-4ec8-a09d-836efcca3fc2</ID>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPath>C:\code\airw\uem\AW.Windows.UEM.API\bin\Debug\netcoreapp2.1\AW.Windows.UEM.Data.dll</CustomAssemblyPath>
    <CustomTypeName>AW.Windows.UEM.Data.UEMContext</CustomTypeName>
    <CustomCxString>Data Source=localhost;Initial Catalog=BaselineDB;User ID=AirWatchAdmin;Password=AirWatchAdmin;</CustomCxString>
  </Connection>
</Query>

from bt in BaselineTemplate
join btp in BaselineTemplatePolicy on bt.ID equals btp.BaselineTemplateID
join p in Policy on btp.PolicyID equals p.ID
join ax in ADMX on p.ADMXID equals ax.ID
join al in ADML on new { ADMXID = ax.ID, LocaleID = 1 } equals new { al.ADMXID, al.LocaleID }
join sr in StringResource on new { ADMLID = al.ID, Key = p.DisplayNameKey } equals new { sr.ADMLID, sr.Key }
where bt.ID == 2 && bt.IsActive
select new { Policy = p, DisplayName = sr.Value }