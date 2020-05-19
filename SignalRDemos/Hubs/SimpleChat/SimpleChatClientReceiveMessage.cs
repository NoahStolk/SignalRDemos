namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientReceiveMessage
	{
		public string UserId { get; set; }
		public string Message { get; set; }

		/// <summary>
		/// We can't use the DateTime type since it isn't compatible with TypeScript.
		/// </summary>
		public string DateTimeString { get; set; }
	}
}