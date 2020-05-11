using Microsoft.AspNetCore.Http;
using SignalRDemos.Hubs.SimpleChat.Storage;
using SignalRDemos.Users;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatHub : AbstractHub<ISimpleChatClient>
	{
		public SimpleChatHub(IHttpContextAccessor context)
			: base(context)
		{
		}

		#region Main events
		public override async Task ClientSendJoin(UserSessionConnectionInfo connectionInfo)
		{
			await base.ClientSendJoin(connectionInfo);

			// Send already-present editing storage data to the member that just joined, if there is any.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				return;

			await BroadcastColors(Clients.Caller);
		}

		public override async Task ClientSendLeave(UserSessionConnectionInfo connectionInfo)
		{
			await base.ClientSendLeave(connectionInfo);

			// Remove the member that just left from storage.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				return;

			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];
			if (!groupData.UserColors.ContainsKey(connectionInfo.User))
				return;

			groupData.UserColors.Remove(connectionInfo.User);

			await BroadcastColors(Clients.Group(GroupName));
		}
		#endregion

		#region Hub-specific events
		public async Task ClientSendMessage(SimpleChatClientSendMessage clientSendMessage)
		{
			await BroadcastMessage(Clients.Group(GroupName), clientSendMessage);
		}

		public async Task ClientSendColor(SimpleChatClientSendColor clientSendColor)
		{
			// If there is no storage for this group, create it.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				SimpleChatStorage.Instance.GroupData.Add(GroupName, new SimpleChatGroupData());

			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];

			// Store the new data.
			groupData.UserColors[clientSendColor.User] = clientSendColor.Color;
			await BroadcastColors(Clients.Group(GroupName));
		}
		#endregion

		#region Responses
		private async Task BroadcastMessage(ISimpleChatClient client, SimpleChatClientSendMessage clientSendMessage)
		{
			SimpleChatClientReceiveMessage clientReceiveMessage = new SimpleChatClientReceiveMessage
			{
				User = clientSendMessage.User,
				Message = clientSendMessage.Message
			};

			await client.ClientReceiveMessage(clientReceiveMessage);
		}

		private async Task BroadcastColors(ISimpleChatClient client)
		{
			SimpleChatClientReceiveColors clientReceiveEvent = new SimpleChatClientReceiveColors
			{
				Colors = SimpleChatStorage.Instance.GroupData[GroupName].UserColors
			};

			await client.ClientReceiveColors(clientReceiveEvent);
		}
		#endregion
	}
}