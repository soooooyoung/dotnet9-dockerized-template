using APIServer.Models.Interfaces;
using APIServer.Utilities;
using OpenAI.Chat;
using APIServer.Models;

namespace APIServer.Services;

public class AIService : IAIService
{
	private static Serilog.ILogger _logger => Serilog.Log.ForContext<AIService>();

	public async Task<(ResultCode, ChatCompletion?)> CompleteChatAsync(ChatRequest request)
	{
		try
		{
			ChatClient chatClient = new(model: request.Model, apiKey: request.ApiKey);
			var result = await chatClient.CompleteChatAsync(request.Prompt);
			return (ResultCode.Success, result);
		}
		catch (Exception e)
		{
			_logger.LogException(e);
			return (ResultCode.UnhandledException, null);
		}
	}
}
