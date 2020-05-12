using SignalRDemos.Users;
using System.Collections.Generic;
using System.Drawing;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatClientReceiveColors
	{
		public Dictionary<User, Color> Colors { get; set; }
	}
}