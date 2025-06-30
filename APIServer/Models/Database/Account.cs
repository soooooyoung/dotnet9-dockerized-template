using Dapper.Contrib.Extensions;

namespace APIServer.Models.Database;

[Table("account")]
public class Account 
{
	public long uid { get; set; }
	[ExplicitKey]
	public string username { get; set; } = String.Empty;
	public string salt { get; set; } = String.Empty;
	public string hashed_password { get; set; } = String.Empty;
	public DateTime create_dt { get; set; }
}
