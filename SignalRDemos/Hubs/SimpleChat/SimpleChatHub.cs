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

		/// <summary>
		/// Handles the join event that occurs when a client requests to join a group.
		/// </summary>
		/// <param name="connectionInfo">The <see cref="UserSessionConnectionInfo"/> object representing this member.</param>
		public override async Task ClientSendJoin(UserSessionConnectionInfo connectionInfo)
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
		/// <param name="connectionInfo">The <see cref="UserSessionConnectionInfo"/> object representing this member.</param>
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

			await Clients.Group(GroupName).BroadcastColors(GroupName);
		}

		#endregion

		#region Hub-specific events

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
			groupData.UserColors[clientSendColor.User] = clientSendColor.Color;
			await Clients.Group(GroupName).BroadcastColors(GroupName);
		}

		#endregion
	}
}