using BlazorClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorClient.Providers;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	private ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
	private DataLoadService _dataLoadService;


	public CustomAuthenticationStateProvider(DataLoadService dataLoadService)
	{
		_dataLoadService = dataLoadService;
	}
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		try
		{
			var result = await _dataLoadService.LoadUserDataAsync();

			if (false == result)
			{
				return new AuthenticationState(_anonymousUser);
			}

			return new AuthenticationState(new ClaimsPrincipal
				(new ClaimsIdentity([new Claim(ClaimTypes.Name, "User")], "auth")));

		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return new AuthenticationState(_anonymousUser);
		}
	}

}
