namespace APIServer.Models.Interfaces;

public interface IAccountService
{
	public Task<ResultCode> RegisterAsync(string username, string password);
}
