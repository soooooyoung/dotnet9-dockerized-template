using Dapper.Contrib.Extensions;

namespace APIServer.Models.Database;

public class Item
{
	[Key]
	public int id { get; set; }
	public string name { get; set; } = String.Empty;
}
