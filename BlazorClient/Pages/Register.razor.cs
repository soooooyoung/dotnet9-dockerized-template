using BlazorClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BlazorClient.Pages;

public partial class Register
{
	[Inject]
	protected AuthService AuthService { get; set; } = null!;
	[Inject]
	protected NavigationManager Navigation
	{
		get; set;
	}
	[Inject]
	protected IToastService ToastService
	{
		get; set;
	}

	protected RegisterRequest User { get; set; } = new RegisterRequest();

	private async Task HandleRegisterAsync()
	{
		try
		{
			var response = await AuthService
				.RegisterAsync(User.Username, User.Password);

			if (ResultCode.Success != response)
			{
				HandleInvalidResponse(response);
			}
			else
			{
				ToastService.ShowSuccess("Account created successfully!");
				RedirectToLogin();
			}
		}
		catch (Exception ex)
		{
			ToastService.ShowError(ex.Message);
		}

	}

	private void RedirectToLogin()
	{
		Navigation.NavigateTo("/login");
	}

	private void HandleInvalidResponse(ResultCode result)
	{
		ToastService.ShowError($"Failed to create account. Error Code:{result}");
	}

}
