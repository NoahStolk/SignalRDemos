using SignalRDemos.Users;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs
{
	/// <summary>
	/// Represents all main events that can be received by clients. This interface enables the <see cref="AbstractHub{TClient}"/> to be strongly-typed.
	/// </summary>
	public interface IClient
	{
		/// <summary>
		/// Broadcasts an event to the current <see cref="IClient"/> that the member represented by the <paramref name="connectionInfo"/> has joined the group.
		/// </summary>
		/// <param name="connectionInfo">The <see cref="UserSessionConnectionInfo"/> object representing this member.</param>
		Task ClientReceiveJoin(UserSessionConnectionInfo connectionInfo);

		/// <summary>
		/// Broadcasts an event to the current <see cref="IClient"/> that the member represented by the <paramref name="connectionInfo"/> has left the group.
		/// </summary>
		/// <param name="connectionInfo">The <see cref="UserSessionConnectionInfo"/> object representing this member.</param>
		Task ClientReceiveLeave(UserSessionConnectionInfo connectionInfo);
	}
}