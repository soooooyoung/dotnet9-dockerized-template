using APIServer.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIServer.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
	private readonly ILoginService _loginService;

	public LoginController(ILoginService loginService)
	{
		_loginService = loginService;
	}

	[HttpPost]
	public async Task<LoginResponse> Login(LoginRequest request)
	{
		LoginResponse response = new();
		(response.Result, var session) = await _loginService.LoginAsync(request.Username, request.Password);

		if (response.Result == ResultCode.Success 
			&& session != null)
		{
			response.Token = session.Token;
			response.Uid = session.Id;
		}

		return response;
	}
}
