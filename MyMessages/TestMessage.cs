namespace MyMessages
{
	using System;
	using NServiceBus;

	[Serializable]
	public class TestMessage : IMessage
	{
		public Guid MessageId { get; set; }
		public Guid AggregateId { get; set; }
		public string DisplayText { get; set; }
	}
}