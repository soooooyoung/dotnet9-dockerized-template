using Dapper.Contrib.Extensions;

namespace APIServer.Models.Database;

public enum MailStatus
{
	Unread = 0,
	Read = 1,
	Received = 2,
}

public class Mail 
{
	[Key]
	public long seq { get; set; }
	public long receiver_account_uid { get; set; }
	public long sender_account_uid { get; set; }
	public int reward_id { get; set; }
	public string title { get; set; } = String.Empty;
	public string content { get; set; } = String.Empty;
	public DateTime created_dt { get; set; }
	public DateTime updated_dt { get; set; }
	public MailStatus status_code { get; set; }
}
