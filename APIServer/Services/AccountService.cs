using APIServer.Models.Database;
using APIServer.Models.Interfaces;
using APIServer.Utilities;

namespace APIServer.Services;

public class AccountService : IAccountService
{
	private static Serilog.ILogger _logger => Serilog.Log.ForContext<AccountService>();
	private readonly IAccountRepository _db;
	public AccountService(IAccountRepository db)
	{ 
		_db = db;
	}

	public async Task<ResultCode> RegisterAsync(string username, string password)
	{
		try
		{
			var salt = Security.GenerateSalt();
			var hashed_password = Security.HashPassword(password, salt);

			if (Security.VerifyPassword(password, salt, hashed_password) == false)
			{
				_logger.Error("Password verification failed during registration for user {Username}", username);
				return ResultCode.UnhandledException;
			}

			return await _db.InsertAsync(new Account
			{
				username = username,
				salt = salt,
				hashed_password = hashed_password,
			});
		}
		catch(Exception e)
		{
			_logger.LogException(e);
			return ResultCode.UnhandledException;
		}
	}
}
