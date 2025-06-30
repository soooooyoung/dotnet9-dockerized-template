using APIServer.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIServer.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;
	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpGet]
	public async Task<ResponseBase> Get()
	{
		ResponseBase response = new();

		return response;
	}

	[HttpPost("register")]
	public async Task<RegisterResponse> Register(RegisterRequest request)
	{
		RegisterResponse response = new();
		response.Result = await _accountService.RegisterAsync(request.Username, request.Password);

		return response;
	}
}
