using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalRDemos.Users;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs
{
	public abstract class AbstractHub<TClient> : Hub<TClient>
		where TClient : class, IClient
	{
		private readonly IHttpContextAccessor context;

		protected string GroupName => context.HttpContext.Request.Query["group_name"];

		protected AbstractHub(IHttpContextAccessor context)
		{
			this.context = context;
		}

		/// <summary>
		/// Handles the join event that occurs when a client joins the group.
		/// </summary>
		public virtual async Task ClientSendJoin(UserSessionConnectionInfo sessionInfo)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

			await Clients.Group(GroupName).ClientReceiveJoin(sessionInfo);
		}

		/// <summary>
		/// Handles the leave event that occurs when a client in the group leaves the group.
		/// </summary>
		public virtual async Task ClientSendLeave(UserSessionConnectionInfo sessionInfo)
		{
			await Clients.Group(GroupName).ClientReceiveLeave(sessionInfo);

			await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);
		}
	}
}