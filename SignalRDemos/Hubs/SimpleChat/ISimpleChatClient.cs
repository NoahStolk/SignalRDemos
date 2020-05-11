using System.Threading.Tasks;

namespace SignalRDemos.Hubs.SimpleChat
{
	public interface ISimpleChatClient : IClient
	{
		Task ClientReceiveMessage(SimpleChatClientReceiveMessage clientReceiveMessage);

		Task ClientReceiveColors(SimpleChatClientReceiveColors clientReceiveColors);
	}
}