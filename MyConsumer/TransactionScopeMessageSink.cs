namespace MyConsumer
{
	using System.Transactions;
	using IdempotentConsumer;
	using NServiceBus.MessageSinks;

	public class TransactionScopeMessageSink : IMessageSink
	{
		private readonly TransactionScope scope;
		private readonly ITrackUnitOfWork unitOfWork;

		public TransactionScopeMessageSink(TransactionScope scope, ITrackUnitOfWork unitOfWork)
		{
			this.scope = scope;
			this.unitOfWork = unitOfWork;
		}

		public void Initialize()
		{
		}
		public void Success()
		{
			this.unitOfWork.Complete();
			this.scope.Complete();
		}
		public void Failure()
		{
		}
		public void Dispose()
		{
			this.scope.Dispose();
		}
	}
}