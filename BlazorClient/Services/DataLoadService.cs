using System.Net.Http.Json;

namespace BlazorClient.Services;

public class DataLoadService
{
	private readonly IHttpClientFactory _httpClientFactory;
	
	public DataLoadService(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<bool> LoadUserDataAsync()
	{
		try
		{
			var gameClient = _httpClientFactory.CreateClient("Game");
			var response = await gameClient.GetAsync("/account");

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<ResponseBase>();

				return result != null && ResultCode.Success == result.Result;
			}

			return false;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return false;
		}
	}

}
