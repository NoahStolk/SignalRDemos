using Microsoft.AspNetCore.Http;
using SignalRDemos.Hubs.SimpleChat.Storage;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	public class SimpleChatHub : AbstractHub<ISimpleChatClient>
	{
		public SimpleChatHub(IHttpContextAccessor context)
			: base(context)
		{
		}

		public override async Task ClientSendJoin(User user)
		{
			await base.ClientSendJoin(user);

			// Send already-present editing storage data to the member that just joined, if there is any.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				return;

			await Clients.Caller.BroadcastColors(GroupName);
		}

		public override async Task ClientSendLeave(User user)
		{
			await base.ClientSendLeave(user);

			// Remove the member that just left from storage.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				return;

			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];
			if (!groupData.UserColors.ContainsKey(user.UserId))
				return;

			groupData.UserColors.Remove(user.UserId);

			await Clients.Group(GroupName).BroadcastColors(GroupName);
		}

		public async Task ClientSendMessage(SimpleChatClientSendMessage clientSendMessage)
		{
			await Clients.Group(GroupName).BroadcastMessage(clientSendMessage);
		}

		public async Task ClientSendColor(SimpleChatClientSendColor clientSendColor)
		{
			// If there is no storage for this group, create it.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				SimpleChatStorage.Instance.GroupData.Add(GroupName, new SimpleChatGroupData());

			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];

			// Store the new data.
			groupData.UserColors[clientSendColor.UserId] = clientSendColor.Color;
			await Clients.Group(GroupName).BroadcastColors(GroupName);
		}
	}
}