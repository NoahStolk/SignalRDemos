namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendMessage : IClientSendEvent
	{
		public string UserId { get; set; }

		public string Message { get; set; }
	}
}