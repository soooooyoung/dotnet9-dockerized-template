using APIServer.Models.Database;
using APIServer.Models.Interfaces;
using APIServer.Utilities;
using Dapper.Contrib.Extensions;

namespace APIServer.Repositories;

public sealed class AccountRepository(IConfiguration configuration) : RepositoryBase<Account>(configuration), IAccountRepository
{
	public async Task<ResultCode> InsertAsync(Account account)
	{
		try
		{
			using var conn = await GetConnectionAsync();
			var count = await conn.InsertAsync(account);
			return count > 0 ? ResultCode.Success : ResultCode.DbInsertAccountError;
		}
		catch (Exception ex)
		{
			_logger.LogException(ex);
			return ResultCode.DbInsertAccountException;
		}
	}

	public async Task<ResultCode> DeleteAsync(long uid)
	{
		try
		{
			using var conn = await GetConnectionAsync();
			var result = await conn.DeleteAsync<Account>(
				new Account() { uid = uid }
				);

			return result ? ResultCode.Success : ResultCode.DbDeletionError;
		}
		catch (Exception ex)
		{
			_logger.LogException(ex);
			return ResultCode.DbDeletionException;
		}
	}

	public async Task<Account?> GetAsync(string username)
	{
		try
		{
			using var conn = await GetConnectionAsync();
			var account = await conn.GetAsync<Account>(username);
			return account;
		}
		catch (Exception ex)
		{
			_logger.LogException(ex);
			return null;
		}
	}
}
