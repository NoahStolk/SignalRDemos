using System;
using System.Collections.Generic;

namespace SignalRDemos.Hubs.SimpleChat.Storage
{
	public sealed class SimpleChatStorage
	{
		public Dictionary<string, SimpleChatGroupData> GroupData { get; private set; } = new Dictionary<string, SimpleChatGroupData>();

		private static readonly Lazy<SimpleChatStorage> lazy = new Lazy<SimpleChatStorage>(() => new SimpleChatStorage());
		public static SimpleChatStorage Instance => lazy.Value;

		private SimpleChatStorage()
		{
		}
	}
}