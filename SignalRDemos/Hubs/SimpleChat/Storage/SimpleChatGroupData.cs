using System.Collections.Generic;

namespace SignalRDemos.Hubs.SimpleChat.Storage
{
	public class SimpleChatGroupData
	{
		public List<User> Users { get; set; } = new List<User>();
	}
}