using System.Threading.Tasks;

namespace SignalRDemos.Hubs
{
	/// <summary>
	/// Represents all main events that can be received by clients. This interface enables the <see cref="AbstractHub{TClient}"/> to be strongly-typed.
	/// </summary>
	public interface IClient
	{
		/// <summary>
		/// Broadcasts an event to the current <see cref="IClient"/> that the client represented by the <paramref name="user"/> parameter has joined the group.
		/// </summary>
		Task ClientReceiveJoin(User user);

		/// <summary>
		/// Broadcasts an event to the current <see cref="IClient"/> that the client represented by the <paramref name="user"/> parameter has left the group.
		/// </summary>
		Task ClientReceiveLeave(User user);
	}
}