using SignalRDemos.Users;
using System.Collections.Generic;
using System.Drawing;

namespace SignalRDemos.Hubs.SimpleChat.Storage
{
	public class SimpleChatGroupData
	{
		public Dictionary<User, Color> UserColors { get; set; } = new Dictionary<User, Color>();
	}
}