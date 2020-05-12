using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	/// <summary>
	/// This interface enables the <see cref="SimpleChatHub"/> to be strongly-typed.
	/// </summary>
	public interface ISimpleChatClient : IClient
	{
		/// <summary>
		/// Broadcasts a <see cref="SimpleChatClientReceiveMessage"/> to the current <see cref="ISimpleChatClient"/>.
		/// </summary>
		Task ClientReceiveMessage(SimpleChatClientReceiveMessage clientReceiveMessage);

		/// <summary>
		/// Broadcasts a <see cref="SimpleChatClientReceiveColors"/> to the current <see cref="ISimpleChatClient"/>.
		/// </summary>
		Task ClientReceiveColors(SimpleChatClientReceiveColors clientReceiveColors);
	}
}