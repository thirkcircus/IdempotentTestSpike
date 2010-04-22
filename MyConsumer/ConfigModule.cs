namespace MyConsumer
{
	using System.Transactions;
	using Autofac.Builder;
	using IdempotentConsumer;
	using NServiceBus.MessageSinks;

	public class ConfigModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder
				.Register(c => new TransactionScopeMessageSink(
					new TransactionScope(), c.Resolve<ITrackUnitOfWork>()))
				.As<IMessageSink>()
				.ContainerScoped();

			builder
				.Register(c => new TestMessageHandler())
				.As<TestMessageHandler>()
				.OnActivating(ActivatingHandler.InjectUnsetProperties)
				.ContainerScoped()
				.ExternallyOwned();
		}
	}
}