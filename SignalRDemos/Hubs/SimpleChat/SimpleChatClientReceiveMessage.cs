using SignalRDemos.Users;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientReceiveMessage
	{
		public User User { get; set; }
		public string Message { get; set; }
	}
}