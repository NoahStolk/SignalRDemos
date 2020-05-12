using SignalRDemos.Users;
using System.Drawing;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendColor : IClientSendEvent
	{
		public User User { get; set; }

		public Color Color { get; set; }
	}
}