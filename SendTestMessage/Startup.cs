namespace SendTestMessage
{
	using System;
	using MyMessages;
	using NServiceBus;

	public class Startup : IWantToRunAtStartup
	{
		public IBus Bus { get; set; }

		public void Run()
		{
			while (true)
			{
				Console.Write("Press enter to send a duplicate message...");
				Console.ReadLine();
				this.Bus.Send(new TestMessage
				{
					MessageId = new Guid("00000000-033F-42ea-A13F-2C2473F37116"),
					AggregateId = new Guid("11111111-0A2F-4949-8914-201B375D3A65"),
					DisplayText = "Hello, World!"
				});
			}
		}

		public void Stop()
		{
		}
	}
}