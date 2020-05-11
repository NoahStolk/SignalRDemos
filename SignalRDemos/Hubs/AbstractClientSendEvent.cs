using SignalRDemos.Users;

namespace SignalRDemos.Hubs
{
	public abstract class AbstractClientSendEvent
	{
		public User User { get; set; }
	}
}