using System.Security.Cryptography;
using System.Text;

namespace APIServer.Utilities;

public static class Security
{
	public static string GenerateToken()
	{
		return GenerateSalt(32);
	}

	/// <summary>
	/// Generates a cryptographically secure random salt.
	/// </summary>
	public static string GenerateSalt(int size = 32)
	{
		var saltBytes = new byte[size];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(saltBytes);
		return Convert.ToHexString(saltBytes); // Uppercase hex
	}

	/// <summary>
	/// Hashes the password with the provided salt using SHA256.
	/// </summary>
	public static string HashPassword(string password, string salt)
	{
		if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(salt))
			throw new ArgumentException("Password and salt must not be null or empty.");

		string salted = salt + password;
		byte[] bytes = Encoding.UTF8.GetBytes(salted);
		byte[] hash = SHA256.HashData(bytes);
		return Convert.ToHexString(hash); // Hex-encoded 256-bit hash
	}

	/// <summary>
	/// Verifies if the provided password and salt hash to the same value as the stored hash.
	/// </summary>
	public static bool VerifyPassword(string password, string salt, string storedHash)
	{
		if (string.IsNullOrWhiteSpace(storedHash))
			return false;

		string computedHash = HashPassword(password, salt);
		return computedHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
	}
}