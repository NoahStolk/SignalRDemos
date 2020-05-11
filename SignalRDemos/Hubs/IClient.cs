using SignalRDemos.Users;
using System.Threading.Tasks;

namespace SignalRDemos.Hubs
{
	public interface IClient
	{
		Task ClientReceiveJoin(UserSessionConnectionInfo connectionInfo);

		Task ClientReceiveLeave(UserSessionConnectionInfo connectionInfo);
	}
}