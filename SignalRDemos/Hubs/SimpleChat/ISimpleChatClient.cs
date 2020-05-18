using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	/// <summary>
	/// This interface enables the <see cref="SimpleChatHub"/> to be strongly-typed.
	/// </summary>
	public interface ISimpleChatClient : IClient
	{
		/// <summary>
		/// Broadcasts a <see cref="SimpleChatClientReceiveMessage"/> event to the current <see cref="ISimpleChatClient"/>.
		/// </summary>
		Task ClientReceiveMessage(SimpleChatClientReceiveMessage clientReceiveMessage);

		/// <summary>
		/// Broadcasts a <see cref="SimpleChatClientReceiveColors"/> event to the current <see cref="ISimpleChatClient"/>.
		/// </summary>
		Task ClientReceiveColors(SimpleChatClientReceiveColors clientReceiveColors);

		/// <summary>
		/// Broadcasts a <see cref="SimpleChatClientReceiveAvatars"/> event to the current <see cref="ISimpleChatClient"/>.
		/// </summary>
		Task ClientReceiveAvatars(SimpleChatClientReceiveAvatars clientReceiveAvatars);
	}
}