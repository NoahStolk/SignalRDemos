namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendColor : IClientSendEvent
	{
		public string UserId { get; set; }

		public string Color { get; set; }
	}
}