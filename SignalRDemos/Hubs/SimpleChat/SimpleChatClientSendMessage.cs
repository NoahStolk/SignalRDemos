namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendMessage : AbstractClientSendEvent
	{
		public string Message { get; set; }
	}
}