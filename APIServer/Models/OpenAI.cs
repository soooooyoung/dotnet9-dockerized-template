using OpenAI.Chat;

namespace APIServer.Models;

public class ChatRequest
{
	public string Model { get; set; } = String.Empty;
	public string ApiKey { get; set; } = String.Empty;
	public string Prompt { get; set; } = String.Empty;
}

public class ChatResponse : ResponseBase
{
	public ChatCompletion? Completion { get; set; }
}