namespace APIServer.Models.Interfaces;

public interface ILoginService
{
	public Task<(ResultCode, Session?)> LoginAsync(string username, string password);
}
