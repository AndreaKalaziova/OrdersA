using Orders.Api.Models;
using System.Collections.Concurrent;

namespace Orders.Api.Managers
{
	public class PaymentQueueManager
	{
		/// <summary>
		///  an in-memory queue manager; thread-safe  enqueue and dequeue operations to indicate it uses concurrency-safe constructs 
		///  like ConcurrentQueue.
		/// </summary>
		
		//A thread-safe queue to store payment information in the form of PaymentInfoDTO
		private readonly ConcurrentQueue<PaymentInfoDTO> queue = new();
		// A semaphore used to signal when an item is available in the queue to be processed.
		private readonly SemaphoreSlim signal = new(0);

		/// <summary>
		/// add new payment info into the queue for processing
		/// </summary>
		/// <param name="paymentInfo"></param>
		public void Enqueue(PaymentInfoDTO paymentInfo)
		{
			queue.Enqueue(paymentInfo); // Enqueue a new payment info
			signal.Release();           // signals that there is a new item to be processed
		}

		/// <summary>
		/// async. dequeues a payment info; if no item in the queue, the methid will wait until an payment is enqueued
		/// </summary>
		/// <param name="cancellationToken">token used to cancel the waiting task if needed</param>
		/// <returns>a task that resolve to the next payment info from the queue</returns>
		
		public async Task<PaymentInfoDTO> DequeueAsync(CancellationToken cancellationToken)
		{
			// Wait asynchronously for an item to be available in the queue.
			await signal.WaitAsync(cancellationToken);
			// Dequeue the item and return it.
			queue.TryDequeue(out var paymentInfo);
			return paymentInfo;
		}
	}
}
