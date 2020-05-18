using SignalRDemos.Hubs.SimpleChat.Storage;
using System.Collections.Generic;
using System.Linq;
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
				Message = clientSendMessage.Message
			};

			await client.ClientReceiveMessage(clientReceiveMessage);
		}

		public static async Task BroadcastColors(this ISimpleChatClient client, string groupName)
		{
			if (!SimpleChatStorage.Instance.GroupData.TryGetValue(groupName, out SimpleChatGroupData groupData))
				return;

			SimpleChatClientReceiveColors clientReceiveEvent = new SimpleChatClientReceiveColors
			{
				Colors = groupData.Users.Select(u => new KeyValuePair<string, string>(u.UserId, u.Color)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
			};

			await client.ClientReceiveColors(clientReceiveEvent);
		}

		public static async Task BroadcastAvatars(this ISimpleChatClient client, string groupName)
		{
			if (!SimpleChatStorage.Instance.GroupData.TryGetValue(groupName, out SimpleChatGroupData groupData))
				return;

			SimpleChatClientReceiveAvatars clientReceiveEvent = new SimpleChatClientReceiveAvatars
			{
				Avatars = groupData.Users.Select(u => new KeyValuePair<string, char>(u.UserId, u.Avatar)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
			};

			await client.ClientReceiveAvatars(clientReceiveEvent);
		}
	}
}