public class RegisterRequest
{
	public string Username { get; set; } = String.Empty;
	public string Password { get; set; } = String.Empty;
}

public class RegisterResponse : ResponseBase
{
}
