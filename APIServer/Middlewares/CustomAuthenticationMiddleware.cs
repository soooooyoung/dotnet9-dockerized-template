using APIServer.Models.Interfaces;

namespace APIServer.Middlewares;

public class CustomAuthenticationMiddleware
{
	private readonly IRedisRepository _cacheRepository;
	private readonly RequestDelegate _next;

	public CustomAuthenticationMiddleware(RequestDelegate next, IRedisRepository cacheRepository)
	{
		_cacheRepository = cacheRepository;
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			if (IsIgnorePath(context.Request.Path.Value))
			{
				await _next(context);
				return;
			}

			var hasToken = context.Request.Headers.TryGetValue("Authorization", out var bearer);	
			var hasUid = context.Request.Headers.TryGetValue("Uid", out var stringUid);

			if (hasToken && long.TryParse(stringUid, out var uid))
			{

				var session = await _cacheRepository.GetSessionAsync(uid);
				if (session != null)
				{
					var token = bearer.ToString()?.Replace("Bearer ", String.Empty);
					if (session.Token == token)
					{
						context.Items["Session"] = session;
						await _next(context);
						return;
					}
				}
			}

			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("Unauthorized");
		}
		catch (Exception)
		{
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsync("Internal Server Error");
		}
	}

	private static bool IsIgnorePath(string? path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return false;
		}

		if (string.Compare(path, "/login", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}

		if (string.Compare(path, "/account/register", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}

		return false;
	}
}

