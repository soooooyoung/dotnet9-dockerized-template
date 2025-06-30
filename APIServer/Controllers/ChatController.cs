using APIServer.Models.Interfaces;
using APIServer.Utilities;
using Microsoft.AspNetCore.Mvc;
using APIServer.Models;

namespace APIServer.Controllers;

[Route("[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
	private static Serilog.ILogger _logger => Serilog.Log.ForContext<ChatController>();
	private readonly IAIService _aiService;

	public ChatController(IAIService aiService)
	{
		_aiService = aiService;
	}


	[HttpPost("chat")]
	public async Task<ChatResponse> Chat([FromBody] ChatRequest request)
	{
		var response = new ChatResponse();
		(response.Result, response.Completion) = await _aiService.CompleteChatAsync(request);
		if (response.Result != ResultCode.Success)
		{
			_logger.LogError(response.Result, "chat completion failed");
		}
		else
		{
			_logger.LogInfo("chat completion succeeded");
		}

		return response;
	}
}
