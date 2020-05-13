using SignalRDemos.Hubs.SimpleChat.Storage;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	public static class SimpleChatClientExtensions
	{
		public static async Task BroadcastMessage(this ISimpleChatClient client, SimpleChatClientSendMessage clientSendMessage)
		{
			SimpleChatClientReceiveMessage clientReceiveMessage = new SimpleChatClientReceiveMessage
			{
				User = ((IClientSendEvent)clientSendMessage).User,
				Message = clientSendMessage.Message
			};

			await client.ClientReceiveMessage(clientReceiveMessage);
		}

		public static async Task BroadcastColors(this ISimpleChatClient client, string groupName)
		{
			SimpleChatClientReceiveColors clientReceiveEvent = new SimpleChatClientReceiveColors
			{
				Colors = SimpleChatStorage.Instance.GroupData[groupName].UserColors
			};

			await client.ClientReceiveColors(clientReceiveEvent);
		}
	}
}