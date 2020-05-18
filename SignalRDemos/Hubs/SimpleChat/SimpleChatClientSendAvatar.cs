namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendAvatar : IClientSendEvent
	{
		public string UserId { get; set; }

		public char Avatar { get; set; }
	}
}