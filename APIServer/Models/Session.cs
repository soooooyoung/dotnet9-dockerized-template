namespace APIServer.Models;

public class Session
{
	public string Token { get; set; } = String.Empty;
	public long Id
	{
		get; set;
	}
	public string Username { get; set; } = String.Empty;
}
