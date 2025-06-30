using APIServer.Utilities;
using MySqlConnector;

namespace APIServer.Repositories;

public abstract class RepositoryBase<T>
{
	private readonly string _connectionString = String.Empty;
	protected static Serilog.ILogger _logger => Serilog.Log.ForContext<RepositoryBase<T>>();

	public RepositoryBase(IConfiguration config)
	{
		var connectionString = config.GetConnectionString("GameDb");
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException("Connection string is empty");
		}

		_connectionString = connectionString;
	}

	protected async Task<MySqlConnection?> GetConnectionAsync()
	{
		try
		{
			var conn = new MySqlConnection(_connectionString);
			await conn.OpenAsync();
			return conn;
		}
		catch (Exception ex)
		{
			_logger.LogException(ex, "Failed to open MySQL connection");
			return null;
		}
	}
}
