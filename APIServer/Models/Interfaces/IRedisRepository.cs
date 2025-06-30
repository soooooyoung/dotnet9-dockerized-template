namespace APIServer.Models.Interfaces
{
	public interface IRedisRepository
	{
		Task<Session?> GetSessionAsync(long id);
		Task<ResultCode> SetSessionAsync(Session session, TimeSpan? expiry = null);
	}
}
