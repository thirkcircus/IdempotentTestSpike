namespace MyConsumer
{
	using System;
	using IdempotentConsumer.Core;
	using MyMessages;

	public class TestMessageHandler : ProactiveMessageHandler<TestMessage>
	{
		protected override void HandleMessage(TestMessage message)
		{
			this.Bus.Send(new ReceivedMessage
			{
				ValueRecevied = message.DisplayText,
				Processed = DateTime.Now
			});
		}

		protected override Guid GetAggregateId(TestMessage message)
		{
			return message.AggregateId;
		}
		protected override Guid GetMessageId(TestMessage message)
		{
			return message.MessageId;
		}
	}
}