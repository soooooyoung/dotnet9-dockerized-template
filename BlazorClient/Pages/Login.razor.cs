using BlazorClient.Providers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using BlazorClient.Services;
using BlazorClient.Models;

namespace BlazorClient.Pages;

public partial class Login
{
	[Inject]
	IToastService ToastService { get; set;}
	[Inject]
	ILocalStorageService LocalStorage { get; set;}
	[Inject]
	AuthenticationStateProvider AuthenticationStateProvider { get; set; }
	[Inject]
	AuthService AuthService { get; set; }
	[Inject]
	NavigationManager Navigation { get; set; }
	[Inject]
	protected LoadingStateProvider LoadingStateProvider { get; set;}

	protected LoginForm User { get; set; } = new LoginForm();

	private async Task HandleLoginAsync()
	{
		try
		{
			LoadingStateProvider?.SetLoading(true);

			var (result, response) = await AuthService.LoginAsync(User.Username, User.Password);

			if (ResultCode.Success != result)
			{
				HandleInvalidResponse(result);
			}
			else
			{
				await LocalStorage.SetItemAsStringAsync("token", response.Token);
				await LocalStorage.SetItemAsStringAsync("uid", response.Uid.ToString());
				ToastService.ShowSuccess("Login successful!");
				Navigation.NavigateTo("/", true);
			}
		}
		catch (Exception ex)
		{
			ToastService.ShowError(ex.Message);
		}
		finally
		{
			LoadingStateProvider.SetLoading(false);
		}
	}
	private void RedirectToRegister()
	{
		Navigation.NavigateTo("/register");
	}

	private void HandleInvalidResponse(ResultCode error)
	{
		ToastService.ShowError($"Failed to login. ResultCode:{error}");
	}
}
