using Reinforced.Typings.Fluent;
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

                // SimpleChat
                typeof(SimpleChatClientReceiveColors),
				typeof(SimpleChatClientSendColor),
				typeof(SimpleChatClientReceiveMessage),
				typeof(SimpleChatClientSendMessage)
			},
			b => b.WithPublicProperties().DontIncludeToNamespace());
		}
	}
}