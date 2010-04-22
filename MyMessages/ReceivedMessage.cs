namespace MyMessages
{
	using System;
	using NServiceBus;

	[Serializable]
	public class ReceivedMessage : IMessage
	{
		public string ValueRecevied { get; set; }
		public DateTime Processed { get; set; }
	}
}