using System.Net.Http.Json;
namespace BlazorClient.Services;


public class AuthService
{
	private readonly IHttpClientFactory _httpClientFactory;
	public AuthService(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<(ResultCode, LoginResponse)> LoginAsync(string username, string password)
	{
		var response = new LoginResponse();
		try
		{
			var gameClient = _httpClientFactory.CreateClient("Game");
			var gameResult = await gameClient.PostAsJsonAsync(
				"/login", new LoginRequest
				{
					Username = username,
					Password = password
				});

			if (false == gameResult.IsSuccessStatusCode)
			{
				return (ResultCode.LoginFailInvalidResponse, response);
			}

			var result = await gameResult.Content.ReadFromJsonAsync<LoginResponse>();

			if (result == null)
			{
				return (ResultCode.LoginFailInvalidResponse, response);
			}

			return (result.Result, result);
		}
		catch (Exception e)
		{
			return (ResultCode.LoginException, response);
		}
	}

	public async Task<ResultCode> RegisterAsync(string username, string password)
	{
		try
		{
			var gameClient = _httpClientFactory.CreateClient("Game");
			var result = await gameClient.PostAsJsonAsync(
				"account/register", new RegisterRequest
				{
					Username = username,
					Password = password
				});

			if (true == result.IsSuccessStatusCode)
			{
				var response = await result.Content.ReadFromJsonAsync<RegisterResponse>();
				if (response == null)
				{
					return ResultCode.InvalidResponse;
				}

				return response.Result;
			}

			return ResultCode.RegisterFail;
		}
		catch (Exception e)
		{
			return ResultCode.RegisterFailException;
		}
	}

	public async Task LogoutAsync()
	{
		try
		{
			var gameClient = _httpClientFactory.CreateClient("Game");
			await gameClient.PostAsync("/logout", null);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
	}
}