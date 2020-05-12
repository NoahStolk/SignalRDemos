using SignalRDemos.Users;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendMessage : IClientSendEvent
	{
		public User User { get; set; }

		public string Message { get; set; }
	}
}