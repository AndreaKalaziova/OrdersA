using Microsoft.EntityFrameworkCore;
using Orders.Data.Interfaces;
using Orders.Data.Models;

namespace Orders.Data.Repositories
{
	/// <summary>
	/// repository class for managing Order entities in db; inherits CRUD operations from BaseRepository
	/// </summary>
	public class orderRepository : BaseRepository<Order>, IOrderRepository
	{
		public orderRepository(OrdersDbContext ordersDbContext) : base(ordersDbContext)
		{ }

		/// <summary>
		/// return all orders 
		/// </summary>
		/// <returns>List of orders</returns>
		public async Task<IList<Order>> GetAllAsync()
		{
			return await dbSet
				.Include(o => o.OrderItems)     // Include OrderItems
				.ToListAsync(); 
		}

		/// <summary>
		/// Checks if an order with the given OrderNumber exists in the repository.
		/// </summary>
		/// <param name="orderNumber">The OrderNumber to check.</param>
		/// <returns>True if an order with the OrderNumber exists; otherwise, false.</returns>
		public bool ExistsWithOrderNumber(ulong orderNumber)
		{
			// Use a query to check for existence
			return ordersDbContext.Orders.Any(i => i.OrderNumber == orderNumber);
		}

		/// <summary>
		/// metchod to fetch order by its Number
		/// </summary>
		/// <param name="orderNumber"></param>
		/// <returns></returns>
		public async Task<Order?> GetOrderByNumberAsync(uint orderNumber)
		{
			return await dbSet
				.Include(o => o.OrderItems)
				.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
		}

		/// <summary>
		/// update existing order in db with the given details
		/// </summary>
		/// <param name="order">object with updated information</param>
		/// <returns>updated order</returns>
		public async Task<Order> UpdateAsync(Order order)
		{
			dbSet.Update(order);
			await ordersDbContext.SaveChangesAsync();
			return order;
		}

	}
}
