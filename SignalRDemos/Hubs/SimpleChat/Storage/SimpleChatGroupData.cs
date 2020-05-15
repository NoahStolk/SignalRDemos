using System.Collections.Generic;

namespace SignalRDemos.Hubs.SimpleChat.Storage
{
	public class SimpleChatGroupData
	{
		public Dictionary<string, string> UserColors { get; set; } = new Dictionary<string, string>();
	}
}