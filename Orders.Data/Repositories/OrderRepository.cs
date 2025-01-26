using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
				.ThenInclude(oi => oi.Product)	// include Product into OrderItem
				.ToListAsync();
		}

		/// <summary>
		/// insert new order to db
		/// </summary>
		/// <param name="order"></param>
		/// <returns>newly added order</returns>
		public async Task<Order> InsertAsync(Order order)
		{ 
			EntityEntry<Order> entityEntry = await dbSet.AddAsync(order);   //add the order entity to dbSet
			await ordersDbContext.SaveChangesAsync();						// save changes to db 
			return entityEntry.Entity;										// return the added order entuity
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
				.ThenInclude(oi => oi.Product)
				.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
		}

		/// <summary>
		/// Checks if an order with the given OrderNumber exists in the repository.
		/// </summary>
		/// <param name="orderNumber">The OrderNumber to check.</param>
		/// <returns>True if an order with the OrderNumber exists; otherwise, false.</returns>
		public bool ExistsWithOrderNumber(ulong orderNumber)
		{
			// Use dbSet for a query to check for existence
			return dbSet.Any(i => i.OrderNumber == orderNumber);
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
