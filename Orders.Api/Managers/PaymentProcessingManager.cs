using Orders.Api.Interfaces;
using System;

namespace Orders.Api.Managers
{
	/// <summary>
	/// a background service; processing payment data from the queue and updating the system state accordingly.
	/// </summary>
	public class PaymentProcessingManager : BackgroundService
	{
		private readonly PaymentQueueManager queueManager;          // The manager that handles the in-memory queue for payment information.
		private readonly IServiceScopeFactory scopeFactory;         // Factory to create service scopes for dependency injection.
		private readonly ILogger<PaymentProcessingManager> logger;  // Logger to record information and errors during the payment processing.

		/// <summary>
		/// Constructor using DI for PaymentProcessingManager.
		/// </summary>
		/// <param name="queueManager"></param>
		/// <param name="scopeFactory"></param>
		/// <param name="logger"></param>
		public PaymentProcessingManager(PaymentQueueManager queueManager, IServiceScopeFactory scopeFactory, ILogger<PaymentProcessingManager> logger)
		{
			this.queueManager = queueManager; //Instance of PaymentQueueManager to manage queued payment information.
			this.scopeFactory = scopeFactory; //Instance of IServiceScopeFactory to resolve scoped services (hosted, singleton)
			this.logger = logger;				// logger to log info and errors
		}

		/// <summary>
		/// The main loop of the background service that processes payment information.
		/// It runs indefinitely until the service is stopped, dequeuing payment informatio 
		/// from the queue and processing it. It also handles errors during processing.
		/// </summary>
		/// <param name="stoppingToken">Token to signal when the background service should stop.</param>
		/// <returns></returns>
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// Keep processing payments until cancellation is requested.
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					// Dequeue and process payment info
					var paymentInfo = await queueManager.DequeueAsync(stoppingToken);

					// Create a new service scope to resolve dependencies that are scoped (e.g., order management service).
					using var scope = scopeFactory.CreateScope();
					var orderManager = scope.ServiceProvider.GetRequiredService<IOrderManager>();

					// Update order state as per payment info 
					await orderManager.UpdateOrderStateAsync(paymentInfo);
					//log the successfull processing of yhe payment
					logger.LogInformation($"Zpracovaná platba pro číslo objednávky: {paymentInfo.OrderNumber}");
				}
				catch (Exception ex)
				{
					// error log
					logger.LogError(ex, "Chyba při zpracování informací o platbě");
				}
			}
		}
	}

}
