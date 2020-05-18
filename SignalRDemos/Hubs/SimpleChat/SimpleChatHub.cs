using Microsoft.AspNetCore.Http;
using SignalRDemos.Hubs.SimpleChat.Storage;
using System.Linq;
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
			if (!SimpleChatStorage.Instance.GroupData.TryGetValue(GroupName, out SimpleChatGroupData groupData))
				return;

			if (groupData.Users.Contains(user))
			{
				groupData.Users.Remove(user);

				await Clients.Group(GroupName).BroadcastColors(GroupName);
			}
		}

		/// <summary>
		/// Broadcasts the message.
		/// </summary>
		public async Task ClientSendMessage(SimpleChatClientSendMessage clientSendMessage)
		{
			await Clients.Group(GroupName).BroadcastMessage(clientSendMessage);
		}

		/// <summary>
		/// Updates the storage and broadcasts it.
		/// </summary>
		public async Task ClientSendColor(SimpleChatClientSendColor clientSendColor)
		{
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				SimpleChatStorage.Instance.GroupData.Add(GroupName, new SimpleChatGroupData());
			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];

			User user = groupData.Users.FirstOrDefault(u => u.UserId == clientSendColor.UserId);
			if (user == null)
				user = new User { UserId = clientSendColor.UserId, Color = clientSendColor.Color };
			else
				user.Color = clientSendColor.Color;

			await Clients.Group(GroupName).BroadcastColors(GroupName);
		}

		/// <summary>
		/// Updates the storage and broadcasts it.
		/// </summary>
		public async Task ClientSendAvatar(SimpleChatClientSendAvatar clientSendAvatar)
		{
			if (!SimpleChatStorage.Instance.GroupData.ContainsKey(GroupName))
				SimpleChatStorage.Instance.GroupData.Add(GroupName, new SimpleChatGroupData());
			SimpleChatGroupData groupData = SimpleChatStorage.Instance.GroupData[GroupName];

			User user = groupData.Users.FirstOrDefault(u => u.UserId == clientSendAvatar.UserId);
			if (user == null)
				user = new User { UserId = clientSendAvatar.UserId, Avatar = clientSendAvatar.Avatar };
			else
				user.Avatar = clientSendAvatar.Avatar;

			await Clients.Group(GroupName).BroadcastAvatars(GroupName);
		}
	}
}