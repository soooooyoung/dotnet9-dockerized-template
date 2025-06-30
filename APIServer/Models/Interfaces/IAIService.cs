using OpenAI.Chat;

namespace APIServer.Models.Interfaces;

public interface IAIService
{
	public Task<(ResultCode, ChatCompletion?)> CompleteChatAsync(ChatRequest request);
}
