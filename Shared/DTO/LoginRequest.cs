public class LoginRequest
{
	public string Username { get; set; } = String.Empty;
	public string Password { get; set; } = String.Empty;
}

public class LoginResponse : ResponseBase
{
	public string Token { get; set; } = String.Empty;
	public long Uid { get; set; } = 0;
}
