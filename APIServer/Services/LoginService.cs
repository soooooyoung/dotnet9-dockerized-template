using APIServer.Models;
using APIServer.Models.Interfaces;
using APIServer.Utilities;
using Serilog;

namespace APIServer.Services;

public class LoginService : ILoginService
{
	private readonly Serilog.ILogger _logger = Log.ForContext<LoginService>();
	private readonly IAccountRepository _accountDb;
	private readonly IRedisRepository _cache;

	public LoginService(IAccountRepository db, IRedisRepository cache)
	{
		_cache = cache;
		_accountDb = db;
	}

	public async Task<(ResultCode, Session?)> LoginAsync(string username, string password)
	{
		try
		{
			var account = await _accountDb.GetAsync(username);
			if (account == null)
			{
				return (ResultCode.LoginErrorUserNotFound, null);
			}

			if (!Security.VerifyPassword(password, account.salt, account.hashed_password))
			{
				return (ResultCode.LoginErrorPasswordMismatch, null);
			}

			// Generate a new token
			var token = Security.GenerateToken();
			var session = new Session
			{
				Id = account.uid,
				Username = account.username,
				Token = token,
			};

			// Store the session in Redis
			var result = await _cache.SetSessionAsync(session, TimeSpan.FromMinutes(ServerConst.SESSION_EXPIRY_MINUTES));
			return (ResultCode.Success,  session);
		}
		catch (Exception e)
		{
			_logger.LogException(e);
			return (ResultCode.LoginException, null);
		}
	}
}
