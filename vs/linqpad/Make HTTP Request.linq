<Query Kind="Program">
  <NuGetReference Version="105.1.0">RestSharp</NuGetReference>
  <Namespace>RestSharp</Namespace>
  <Namespace>RestSharp.Authenticators</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	var osVersionUUID = Guid.Parse("6628B6E3-3DB5-4225-91B7-0308C25E09DF");
	var q = "account on";
	var language = "en-US";
	var limit = 10;
	
	var response = await GetAllPoliciesAsyncWithHttpInfo(osVersionUUID, q, language, limit);
	response.Dump();
}

public async System.Threading.Tasks.Task<IRestResponse> GetAllPoliciesAsyncWithHttpInfo(Guid? osVersionUUID, string q = null, string language = null, int? limit = null)
{
	// verify the required parameter 'osVersionUUID' is set
	if (osVersionUUID == null)
		throw new Exception("Missing required parameter 'osVersionUUID' when calling CatalogsApi->GetAllPolicies");

	var localVarPath = "/catalogs/{osVersionUUID}/policies";
	var localVarPathParams = new Dictionary<String, String>();
	var localVarQueryParams = new Dictionary<String, String>();
	var localVarHeaderParams = new Dictionary<String, String>();
	var localVarFormParams = new Dictionary<String, String>();
	var localVarFileParams = new Dictionary<String, FileParameter>();
	Object localVarPostBody = null;

	// to determine the Content-Type header
	String localVarHttpContentType = "application/json";

	// to determine the Accept header
	String localVarHttpHeaderAccept = "application/json";
	if (localVarHttpHeaderAccept != null)
		localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

	// set "format" to json by default
	// e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
	localVarPathParams.Add("format", "json");
	if (osVersionUUID != null) localVarPathParams.Add("osVersionUUID", ParameterToString(osVersionUUID)); // path parameter
	if (q != null) localVarQueryParams.Add("q", ParameterToString(q)); // query parameter
	if (language != null) localVarQueryParams.Add("language", ParameterToString(language)); // query parameter
	if (limit != null) localVarQueryParams.Add("limit", ParameterToString(limit)); // query parameter

	// make the HTTP request
	IRestResponse localVarResponse = (IRestResponse)await CallApiAsync(localVarPath,
		Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
		localVarPathParams, localVarHttpContentType);

	int localVarStatusCode = (int)localVarResponse.StatusCode;

	return localVarResponse;
}

public async System.Threading.Tasks.Task<Object> CallApiAsync(
	String path, RestSharp.Method method, Dictionary<String, String> queryParams, Object postBody,
	Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
	Dictionary<String, FileParameter> fileParams, Dictionary<String, String> pathParams,
	String contentType)
{
	var client = new RestClient("https://dev.baselines.vharmonylabs.com");
	client.Authenticator = new Authenticator();

	var request = PrepareRequest(
		path, method, queryParams, postBody, headerParams, formParams, fileParams,
		pathParams, contentType);
		
	var response = await client.ExecuteTaskAsync(request);
	
	return (Object)response;
}

private RestRequest PrepareRequest(
	String path, RestSharp.Method method, Dictionary<String, String> queryParams, Object postBody,
	Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
	Dictionary<String, FileParameter> fileParams, Dictionary<String, String> pathParams,
	String contentType)
{
	var request = new RestRequest(path, method);

	// add path parameter, if any
	foreach (var param in pathParams)
		request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);

	// add header parameter, if any
	foreach (var param in headerParams)
		request.AddHeader(param.Key, param.Value);

	// add query parameter, if any
	foreach (var param in queryParams)
		request.AddQueryParameter(param.Key, param.Value);

	// add form parameter, if any
	foreach (var param in formParams)
		request.AddParameter(param.Key, param.Value);

	// add file parameter, if any
	foreach (var param in fileParams)
	{
		request.AddFile(param.Value.Name, param.Value.Writer, param.Value.FileName, param.Value.ContentType);
	}

	if (postBody != null) // http body (model or byte[]) parameter
	{
		if (postBody.GetType() == typeof(String))
		{
			request.AddParameter("application/json", postBody, ParameterType.RequestBody);
		}
		else if (postBody.GetType() == typeof(byte[]))
		{
			request.AddParameter(contentType, postBody, ParameterType.RequestBody);
		}
	}

	return request;
}

/// <summary>
/// If parameter is DateTime, output in a formatted string (default ISO 8601), customizable with Configuration.DateTime.
/// If parameter is a list, join the list with ",".
/// Otherwise just return the string.
/// </summary>
/// <param name="obj">The parameter (header, path, query, form).</param>
/// <returns>Formatted string.</returns>
public string ParameterToString(object obj)
{
	if (obj is DateTime)
		// Return a formatted date string - Can be customized with Configuration.DateTimeFormat
		// Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
		// https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
		// For example: 2009-06-15T13:45:30.0000000
		return ((DateTime)obj).ToString();
	else if (obj is DateTimeOffset)
		// Return a formatted date string - Can be customized with Configuration.DateTimeFormat
		// Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
		// https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
		// For example: 2009-06-15T13:45:30.0000000
		return ((DateTimeOffset)obj).ToString();
	else if (obj is IList)
	{
		var flattenedString = new StringBuilder();
		foreach (var param in (IList)obj)
		{
			if (flattenedString.Length > 0)
				flattenedString.Append(",");
			flattenedString.Append(param);
		}
		return flattenedString.ToString();
	}
	else
		return Convert.ToString(obj);
}

public class Authenticator : IAuthenticator
{
	public void Authenticate(IRestClient client, IRestRequest request)
	{
		var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ijg5eGhHcUlGbGxqS0ZWM2djYjZxSFktbjNFcyIsIng1YyI6WyJNSUlHekRDQ0JMYWdBd0lCQWdJU0RaVStKVTZPRHUvRUc4YmNHZm9PSVQvWk1Bc0dDU3FHU0liM0RRRUJEVEEyTVRRd01nWURWUVFERXl0QmFYSlhZWFJqYUNCTmFXTnliM05sY25acFkyVWdTV1JsYm5ScGRIa2dTWE56ZFdsdVp5QkRaWEowTUI0WERURTVNREV3TmpFNE1UVXdPRm9YRFRJeU1ERXdOVEU0TVRVd09Gb3dnWk14TXpBeEJnTlZCQU1US2tGcGNsZGhkR05vSUUxcFkzSnZjMlZ5ZG1salpTQkpaR1Z1ZEdsMGVTQkRaWEowYVdacFkyRjBaVEV0TUNzR0ExVUVDaE1rTmpVNU4yTXlNell0T0dKbVpTMDBOR1ZtTFRnM01HSXRPVGs0WXpWbE1qUXdOekEzTVMwd0t3WURWUVFMRXlRMk5UazNZekl6TmkwNFltWmxMVFEwWldZdE9EY3dZaTA1T1Roak5XVXlOREEzTURjd2dnSWlNQTBHQ1NxR1NJYjNEUUVCQVFVQUE0SUNEd0F3Z2dJS0FvSUNBUUNxWXh4Uk1HMW9KZEthRGFUMVJmZGFlbGpmWHhpVmNacVVITHo1Mnc1NDA3cm0yZEdReTZTZEtwaDhHQkVBYmZNL1BxWEJDU2dwWWZDNFA5SjF1aitlaVl4OGR4R3B5NzNEVU4wR2REcFZ4ZkVOTE1NbzNCaHRMVEhUa2ZFNUJIZSt0ZzcwLzNGcnpCVlNjSW9FWm9kWjNKOEVjSmlXMnljQTZaYXQyRTlXVHJrUHZBczFXZzIwR1JrN1F4VWQ0bWJ0S1ZOMUhWU09Ca0hmblZqblF2aGdqVHcyb3JaaG02S0JMcG9BNEZMTnMwbityZkZ4dkpSVE13L1FQdmtBMGQ3L3ZteXhsS3ByS3BlRGlaRUd5Sy9yaHdmVUlsRUQ1YW9qN0hXYW9KZC9EUDlCaSt4UzRibkE3OXNBOExRMmpnWGo3dzc3dlpLL1lqRzNzazF3MDVnSmdJZGZPbW1IRjZxWFpKaW5XTWkyNUdNcjFUSmw4ZnFidVhhWDhJTjIrSUtPWFFYYlBjU0xTandCdVRMMGs3WnFNMVJud2NDVnhWSXUvVUNWSFNTQ1JpUXBBTTd6Ull4aFh5NXdUeFhuazZKRGR4OEVIVWhxcVZScWdSYVR1T0JET2FZdDI4VkplY0ZDTThrRWc4cmhreEN3SE0rclExQUJDQ2llYmZPMkxJcThadHhPMzNVVjBERWJMcHIxa2lUdTZCZUJYWWRmaFEzSmRpYVdsbEtxbHRlMXBuNWtia1BCUHRPWkZqWkE1Y3lac3NoYVJCWTlxQlNSaE9tNHIvQ041dTFuODRqNnNiQ0swaCs2c0R1U25QTHdKUWQ3OG9iN255R3hUZERVam9ja2ZhYjUzZjlHN1k2U3Y2K3hzcm9veXRCOUxmSk5jMnBXcVpxM3RBQWVKUUlEQVFBQm80SUJlRENDQVhRd0hRWURWUjBPQkJZRUZEZWNjalBUK05wNkozdjlMY0kveHdMb0U4bkNNSUcrQmdnckJnRUZCUWNCQVFTQnNUQ0JyakE2QmdnckJnRUZCUWN3QVlZdWFIUjBjRG92TDNOcFoyNXBibWN1WVhkdFpHMHVZMjl0TDFOcFoyNXBibWRUWlhKMmFXTmxMMjlqYzNBdk1qQndCZ2dyQmdFRkJRY3dBb1prYUhSMGNITTZMeTl6YVdkdWFXNW5MbUYzYldSdExtTnZiUzlUYVdkdWFXNW5VMlZ5ZG1salpTOXphV2R1YVc1blkyaGhhVzV6TDJObGNuUXZSamRETWpkRlFUSkdOVUpCTVVJNFEwSXpPVVZCTmtKR01qSkJSall5TlRNM09EY3hSVGhCTlRBT0JnTlZIUThCQWY4RUJBTUNBOWd3SFFZRFZSMGxCQll3RkFZSUt3WUJCUVVIQXdFR0NDc0dBUVVGQndNQ01HTUdBMVVkSXdSY01GcUFGS0pFQU85UzhwQzZVYUhtTW5aZ2o2QXhidUlsb1Rpa05qQTBNVEl3TUFZRFZRUURFeWxCYVhKWFlYUmphQ0JOYVdOeWIzTmxjblpwWTJVZ1NXUmxiblJwZEhrZ1ZISjFjM1FnVW05dmRJSUlBZ0FBQUFBQUFBQXdDd1lKS29aSWh2Y05BUUVOQTRJQ0FRQXZlUGJnZW9ldEdqbnVKWUloelFtY2R5S0dqUnQ1bVBHTnJ0M3pwU0tocWlnQU8vM0FidzhWaUk3aWlTcWJHMXYwb3laM1pyWVUvOHBnWXRsN1FtMlEvS2hnZGpLSEpFUG5IOS93YmF4dUZXejdJemJZSU1aeGMycVAwZUlxalhlT2Z1TjBsWGVIdDRyKzBCdTdSL2ZFM1o0N3FaUGZKUS9rbllUWWZDMEdoOGRaclA1ZEMxSzlqUENkdlR1TFdUOHBMR1ZyaGZwd2ZuT1A4bWdHY2tkM1hLdXdvOHIwM3RSS3hVbDR6S09BUkpoYWl2cTF4bXB1ZnBudlQrT00vVHB1dktOY3AveU94VzN6dVRYc2RGZy80K3lxV0YwcTF6blY5RjNjWThFVGJlL2dmMTZDZmQzbGtqN1dHdmgxZzFnTU5Uc1diNjhMQkREbFhabUhaR0lxWTVyeFcwdXMrNmxHbk1PVURxVGdMdGhIZXRDSVRRbGpEaklGZTFYSkl0Y05DK0xpVXJFcU1MQ1lFcTc4V2prbkt4MlVmd09vTXB6dzNxdXFmcjU3M1J1T1c2dVFGeGVrS2puNzJWTDZPSm9mcVRpbEM2eUk3T0liMlJVSlFVd05oU2o1bUNBRGxBcy81aDMrUGtpR00ydXZjLzh4TDlrUlUxRWdSVlJsYTI4dlcwWTJaUzlNMG05dUl6VW1lQ1dWT2xuN3JCVkhaVnhDSmhEa3EzLzFWQThTNXNOblcvU3JwY09yWXlyajd3VW11b015THE1OVNWN3pHc0QwUFEzRGI3cm5SbHRmMzBVNnQ2OFJjZ1ByS1RzUXBJUjAxVTB2Vm1vWWZ1TmFyUkN6V3FrNGtUTTRFRER3Y04vSTBVcWVzTDdnU1ZSV21qYUh1SmRsUHFLZWtBPT0iXX0.eyJjbGllbnRfaWQiOiIzZDU5MDBhZS0xMTFhLTQ1YmUtOTZiMy1kOWU0NjA2Y2E3OTMiLCJzdWIiOiJ7XCJPcmdhbml6YXRpb25Hcm91cElEXCI6NyxcIkluc3RhbmNlSWRcIjpcIjY1OTdjMjM2LThiZmUtNDRlZi04NzBiLTk5OGM1ZTI0MDcwN1wiLFwiQ3VzdG9tZXJJZFwiOlwiNjU5N2MyMzYtOGJmZS00NGVmLTg3MGItOTk4YzVlMjQwNzA3XCJ9IiwiaW5zdGFuY2VfaWQiOiI2NTk3YzIzNi04YmZlLTQ0ZWYtODcwYi05OThjNWUyNDA3MDciLCJvZ19pZCI6IiIsIm9nX3V1aWQiOiI2NTk3YzIzNi04YmZlLTQ0ZWYtODcwYi05OThjNWUyNDA3MDciLCJvZ19uYW1lIjoiR2xvYmFsIiwicGVybWlzc2lvbiI6WyJWaWV3QmFzZWxpbmVzIiwiTWFuYWdlQmFzZWxpbmVzIl0sImlzcyI6ImU2MWEyMjdhLWRhZjQtNGRkNS1hM2VkLTJlOWYwZmM3ZmIzNSIsImF1ZCI6IkJhc2VsaW5lQVBJIiwiZXhwIjoxNjEzMzI2OTEyLCJuYmYiOjE1NTAxNjgyMTJ9.FyRqQ4bVICznJ6Sbi0Ce0rhKTRwYBSKmjtbfaq3a1jQ07L_bZPGwRvrqiCrgXDd9YZ0_K5ks69YM_qT8bE5POh95iykrSQtKNnwVg9iZj8cO_LJlZ9Zn0FZ4eT0zAAz6ju4f0xu2BHKvS7LyhsJi0fCYfQ-fALmuF6Ajr2qQ0Dftg_v2pqYxUvefRKM-l_UN7bDE8DTOPKX3P-TD8YZr7tY2xH3axQZwYK1_7jlGleMV_ahwobWwff2Fb35fRBuHXhAeOeKBeifC_GntWMZcQWRDqZxPN42THeOguB8ogxl9ly6TPyC7FD-PllK3aonLpNGb8X_2fsCPkG9v_eAFcF43umiv963W68wSd24EFtfQ3RLJI307Kl5K_OEoBnOXz-Ei49VKDuvoM7OCYkfzfrHnubvgBD9W-yuYhDqQQYsLt0WMaXtvYREeP17u9Dl1_TW4Pbd1K1uAzyo3spHgTfqTL_RgMQpMOD4eln0KBPHTqtbyiO_O1jd2wYBk_xh2sfv2JTF3Eo3k2ZOOkPrY9NogA9q_702m_DFD3Z4DsEtBe4wvw7QnFHus1Fw0EppLmwaiiyqcYUN2c52ah0p16JKlXRRpsxgTjLXJOZiJWqbwrjA-wbGxzICR0R5oIoJzRC4uCzaNL8fMXz3aryOLj5AN5Mg8INfRYurp28jotfw";
		request.AddHeader("Authorization", $"Bearer {token}");
	}
}