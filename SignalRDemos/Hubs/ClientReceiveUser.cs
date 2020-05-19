namespace SignalRDemos.Hubs
{
	public class ClientReceiveUser
	{
		public User User { get; set; }

		/// <summary>
		/// We can't use the DateTime type since it isn't compatible with TypeScript.
		/// </summary>
		public string DateTimeString { get; set; }
	}
}