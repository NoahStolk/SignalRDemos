using Microsoft.AspNetCore.SignalR.Client;
using SignalRDemos.Hubs;
using SignalRDemos.Hubs.SimpleChat;
using SignalRDemos.Users;
using System;
using System.Threading.Tasks;

namespace SignalRClient.Code
{
	public sealed class SimpleChatClient
	{
		private HubConnection connection;

		public bool IsConnected => connection?.State == HubConnectionState.Connected;

		private static readonly Lazy<SimpleChatClient> lazy = new Lazy<SimpleChatClient>(() => new SimpleChatClient());
		public static SimpleChatClient Instance => lazy.Value;

		private SimpleChatClient()
		{
		}

		public async Task ToggleConnection(
			Action<UserSessionConnectionInfo> receiveJoin,
			Action<UserSessionConnectionInfo> receiveLeave,
			Action<SimpleChatClientReceiveMessage> receiveMessage,
			Action<SimpleChatClientReceiveColors> receiveColors)
		{
			UserSessionConnectionInfo sessionInfo = new UserSessionConnectionInfo { User = new User { UserId = "MonitorId", UserName = "Monitor" } };

			if (IsConnected)
			{
				await connection.InvokeAsync(nameof(SimpleChatHub.ClientSendLeave), sessionInfo);
				await connection.StopAsync();
			}
			else
			{
				connection = new HubConnectionBuilder()
					.WithUrl(Constants.SimpleChatHubUrl)
					.Build();

				connection.On<UserSessionConnectionInfo>(nameof(IClient.ClientReceiveJoin), (param) => receiveJoin(param));
				connection.On<UserSessionConnectionInfo>(nameof(IClient.ClientReceiveLeave), (param) => receiveLeave(param));
				connection.On<SimpleChatClientReceiveMessage>(nameof(ISimpleChatClient.ClientReceiveMessage), (param) => receiveMessage(param));
				connection.On<SimpleChatClientReceiveColors>(nameof(ISimpleChatClient.ClientReceiveColors), (param) => receiveColors(param));

				await connection.StartAsync();
				await connection.InvokeAsync(nameof(SimpleChatHub.ClientSendJoin), sessionInfo);
			}
		}
	}
}