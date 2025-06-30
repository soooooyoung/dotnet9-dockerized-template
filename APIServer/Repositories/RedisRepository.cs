using APIServer.Models;
using APIServer.Models.Interfaces;
using APIServer.Utilities;
using StackExchange.Redis;
using System.Text.Json;

namespace APIServer.Repositories;

public class RedisRepository : IRedisRepository
{
	private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<RedisRepository>();
	private readonly IConnectionMultiplexer _connection;
	private readonly IDatabase _db;

	private static string GenerateSessionKey(long id)
	{
		return $"session:{id}";
	}

	public RedisRepository(IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Redis");
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException("Redis connection string is not configured.");
		}

		_connection = ConnectionMultiplexer.Connect(connectionString);
		_db = _connection.GetDatabase();
	}

	public async Task<Session?> GetSessionAsync(long id)
	{
		try
		{
			var sessionKey = GenerateSessionKey(id);
			var sessionData = await _db.StringGetAsync(sessionKey);
			if (sessionData.IsNullOrEmpty)
			{
				return null;
			}

			return JsonSerializer.Deserialize<Session>(sessionData!);
		}
		catch (Exception ex)
		{
			_logger.LogException(ex);
			return null;
		}
	}

	public async Task<ResultCode> SetSessionAsync(Session session, TimeSpan? expiry)
	{
		try
		{
			var sessionKey = GenerateSessionKey(session.Id);
			var sessionData = JsonSerializer.Serialize(session);
			var result = await _db.StringSetAsync(sessionKey, sessionData, expiry);
			return result ? ResultCode.Success : ResultCode.SessionSetFail;
		}
		catch (Exception ex)
		{
			_logger.LogException(ex);
			return ResultCode.SessionSetException;
		}
	}
}
