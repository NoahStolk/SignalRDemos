using SignalRDemos.Hubs.SimpleChat.Storage;
using System;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	public static class SimpleChatClientExtensions
	{
		public static async Task BroadcastMessage(this ISimpleChatClient client, SimpleChatClientSendMessage clientSendMessage)
		{
			SimpleChatClientReceiveMessage clientReceiveMessage = new SimpleChatClientReceiveMessage
			{
				UserId = clientSendMessage.UserId,
				Message = clientSendMessage.Message,
				DateTimeString = DateTime.Now.ToString()
			};

			await client.ClientReceiveMessage(clientReceiveMessage);
		}

		public static async Task BroadcastUsers(this ISimpleChatClient client, string groupName)
		{
			if (!SimpleChatStorage.Instance.GroupData.TryGetValue(groupName, out SimpleChatGroupData groupData))
				return;

			SimpleChatClientReceiveUsers clientReceiveEvent = new SimpleChatClientReceiveUsers
			{
				Users = groupData.Users
			};

			await client.ClientReceiveUsers(clientReceiveEvent);
		}
	}
}