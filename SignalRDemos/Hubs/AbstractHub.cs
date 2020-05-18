using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs
{
	/// <summary>
	/// Represents the base class for every hub hosted by the service.
	/// </summary>
	/// <typeparam name="TClient">The client interface for this hub. This interface enables the hub to be strongly-typed.</typeparam>
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
		/// Handles the join event that occurs when a client requests to join a group.
		/// </summary>
		public virtual async Task ClientSendJoin(User user)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

			await Clients.Group(GroupName).ClientReceiveJoin(user);
		}

		/// <summary>
		/// Handles the leave event that occurs when a client requests to leave a group.
		/// </summary>
		public virtual async Task ClientSendLeave(User user)
		{
			await Clients.Group(GroupName).ClientReceiveLeave(user);

			await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);
		}
	}
}