using Reinforced.Typings.Fluent;
using SignalRDemos.Hubs;
using SignalRDemos.Hubs.SimpleChat;

namespace SignalRDemos
{
	public static class ReinforcedTypingsConfiguration
	{
		public static void Configure(ConfigurationBuilder builder)
		{
			builder.Global(b => b.CamelCaseForProperties(true).UseModules(true));

			builder.ExportAsInterfaces(new[]
			{
                // Global
				typeof(User),
				typeof(ClientReceiveUser),

                // SimpleChat
				typeof(SimpleChatClientSendColor),
				typeof(SimpleChatClientSendMessage),
				typeof(SimpleChatClientSendAvatar),
				typeof(SimpleChatClientReceiveMessage),
				typeof(SimpleChatClientReceiveUsers)
			},
			b => b.WithPublicProperties().DontIncludeToNamespace());
		}
	}
}