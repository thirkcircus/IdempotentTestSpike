namespace MyConsumer
{
	using Autofac.Builder;
	using IdempotentConsumer.Autofac;
	using NServiceBus;
	using NServiceBus.MessageSinks.AutofacConfiguration;

	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	{
		public void Init()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new MessageSinkConfigurationModule());
			builder.RegisterModule(new IdempotentConfigurationModule());
			builder.RegisterModule(new ConfigModule());

			Configure.With()
				.AutofacBuilder(builder.Build().ThreadScoped())
				.XmlSerializer()
				.MsmqTransport()
				.UnicastBus().DoNotAutoSubscribe();
		}
	}
}