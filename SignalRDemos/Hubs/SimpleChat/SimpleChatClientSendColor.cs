using System.Drawing;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientSendColor : AbstractClientSendEvent
	{
		public Color Color { get; set; }
	}
}