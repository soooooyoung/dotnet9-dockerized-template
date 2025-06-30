using Dapper.Contrib.Extensions;

namespace APIServer.Models.Database;

public class Reward 
{
	[Key]
	public long seq { get; set; }
	public int id { get; set; }
	public int item_id { get; set; }
	public int item_count { get; set; }
}
