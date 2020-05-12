using SignalRDemos.Users;

namespace SignalRDemos.Hubs
{
	public interface IClientSendEvent
	{
		public User User { get; set; }
	}
}