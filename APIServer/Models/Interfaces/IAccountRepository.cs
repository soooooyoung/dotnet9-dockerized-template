using APIServer.Models.Database;

namespace APIServer.Models.Interfaces;

public interface IAccountRepository
{
	public Task<ResultCode> InsertAsync(Account account);
	public Task<ResultCode> DeleteAsync(long uid);
	public Task<Account?> GetAsync(string username);
}
