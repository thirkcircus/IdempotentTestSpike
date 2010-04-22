namespace MyConsumer
{
	using System;
	using System.Transactions;
	using Autofac.Builder;
	using IdempotentConsumer;
	using IdempotentConsumer.Core;
	using NServiceBus;
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
				.Register(c => new DuplicateMessageFilter(c.Resolve<ILoadMessages>(), c.Resolve<IDispatchMessages>()))
				.As<IFilterDuplicateMessages>()
				.ContainerScoped()
				.ExternallyOwned();

			builder
				.Register(c => new IdempotentUnitOfWork(
					c.Resolve<IStoreMessages>(), c.Resolve<IDispatchMessages>(), c.Resolve<IBuildMessages>()))
				.As<ITrackUnitOfWork>()
				.ContainerScoped()
				.ExternallyOwned();

			builder
				.Register(c => new InMemoryStorage())
				.As<IStoreMessages>()
				.As<ILoadMessages>()
				.SingletonScoped()
				.ExternallyOwned();

			builder
				.Register(c => new IdempotentBus(c.Resolve<IBus>(), c.Resolve<ITrackUnitOfWork>()))
				.As<IIdempotentBus>()
				.ContainerScoped()
				.ExternallyOwned();

			builder
				.Register(c => new MessageDispatcher(c.Resolve<IBus>()))
				.As<IDispatchMessages>()
				.ContainerScoped()
				.ExternallyOwned();

			builder
				.Register(c => new MessageBuilder(DateTime.UtcNow))
				.As<IBuildMessages>()
				.As<IRegisterMessageIdentifiers>()
				.ContainerScoped()
				.ExternallyOwned();

			builder
				.Register(c => new TestMessageHandler())
				.As<TestMessageHandler>()
				.OnActivating(ActivatingHandler.InjectUnsetProperties)
				.ContainerScoped()
				.ExternallyOwned();
		}
	}
}