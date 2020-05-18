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

		/// <summary>
		/// Handles the join event that occurs when a client requests to join a group.
		/// </summary>
		/// <param name="connectionInfo">The <see cref="ConnectionInfo"/> object representing this member.</param>
		public override async Task ClientSendJoin(ConnectionInfo connectionInfo)
		{
			await base.ClientSendJoin(connectionInfo);

			// Send already-present editing storage data to the member that just joined, if there is any.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				return;

			await Clients.Caller.BroadcastColors(GroupName);
		}

		/// <summary>
		/// Handles the leave event that occurs when a client requests to leave a group.
		/// </summary>
		/// <param name="connectionInfo">The <see cref="ConnectionInfo"/> object representing this member.</param>
		public override async Task ClientSendLeave(ConnectionInfo connectionInfo)
		{
			await base.ClientSendLeave(connectionInfo);

			// Remove the member that just left from storage.
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				return;

			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];
			if (!groupData.UserColors.ContainsKey(connectionInfo.UserId))
				return;

			groupData.UserColors.Remove(connectionInfo.UserId);

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