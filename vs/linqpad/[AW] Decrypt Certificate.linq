<Query Kind="Program">
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Framework.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Framework.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Logging.Core.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Logging.Core.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Logging.Database.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Logging.Database.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Logging.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatch.Logging.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatchCore.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatchCore.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatchFoundation.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\AirWatchFoundation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\System.IdentityModel.Tokens.Jwt.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\System.IdentityModel.Tokens.Jwt.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\WanderingWiFi.Framework.Business.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\WanderingWiFi.Framework.Business.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\WanderingWiFi.Framework.DBProviderImpl.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\WanderingWiFi.Framework.DBProviderImpl.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\WanderingWiFi.Framework.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\WanderingWiFi.Framework.dll</Reference>
  <Reference Relative="..\..\..\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\Microsoft.IdentityModel.Tokens.dll">C:\code\airw\canonical\AirWatch API\AW.Mdm.Api\AW.Mdm.Api\bin\Microsoft.IdentityModel.Tokens.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>AirWatch.Security.Cryptography</Namespace>
  <Namespace>Microsoft.IdentityModel.Tokens</Namespace>
  <Namespace>System.IdentityModel.Tokens.Jwt</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Security.Claims</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var rawPassword = "awev2:kv0:opjiZp3NrdV617iK:EF7wVAyvJZh+Sf45SC8vrT1vgpBj42R0eajUjmKAHyEUM21o0OWV71P2+jNijbo3Nq6ma8o=";
	// hex code via Fiddler => Tools => TextWizard. Save output to a .pfx file
	var pfxPath = @"c:\Temp\test.pfx";
	// var pfxPath = @"c:\Temp\ms_cert.pfx";
	
	var password = DataEncryption.DecryptString(rawPassword, null);
	// 608b4fb4-36ae-4c25-9445-d3103d3af5b9
	var data = File.ReadAllBytes(pfxPath);
	
	password.Dump();
	
	var x509Store = new X509Certificate2Collection();
	x509Store.Import(data, password, X509KeyStorageFlags.Exportable);
	var certCollection = x509Store.EnsureCleanup();
	

	var cert = certCollection[certCollection.Count - 1];
	cert.Dump();
	CreateAuthToken(cert).Dump();
}

// Define other methods and classes here
private string CreateAuthToken(X509Certificate2 cert)
{
	// Get Environment Info, This contains instance ID and customer ID
	var sub = new
	{
		OrganizationGroupID = 7
	};

	var claims = new List<Claim>
			{
				new Claim("client_id", "test"),
				new Claim("sub", JsonConvert.SerializeObject(sub))
			};

	var header = new JwtHeader(new X509SigningCredentials(cert))
			{
				{
                    // X.509 Certificate Chain Header Parameter
                    "x5c",
                    // this includes public cert or certificate chain corresponding to key used to digitally sign the JWS
                    new[] { Convert.ToBase64String(cert.Export(X509ContentType.Cert)) }
				},
			};

	var payload = new JwtPayload(
		"blah",
		"blah_aud",
		claims,
		// take care of possible clock skew
		DateTime.UtcNow.AddMinutes(-5),
		// keep short expiration time
		DateTime.UtcNow.AddMinutes(10));

	var token = new JwtSecurityToken(header, payload);
	return new JwtSecurityTokenHandler().WriteToken(token);
}
