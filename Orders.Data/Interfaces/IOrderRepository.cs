using Orders.Data.Models;

namespace Orders.Data.Interfaces
{
	public interface IOrderRepository : IBaseRepository<Order>
	{
		/// <summary>
		/// return all orders in db
		/// </summary>
		/// <returns>list of orders</returns>
		Task <IList<Order>> GetAllAsync();

		/// <summary>
		/// insert new order to db
		/// </summary>
		/// <param name="order"></param>
		/// <returns>newly added order</returns>
		Task <Order> InsertAsync (Order order);

		/// <summary>
		/// Checks if an order with the given OrderNumber exists in the repository.
		/// </summary>
		bool ExistsWithOrderNumber(ulong orderNumber);

		/// <summary>
		/// metchod to fetch order by its Number
		/// </summary>
		Task<Order?> GetOrderByNumberAsync(uint orderNumber);

		/// <summary>
		/// update existing order in db with the given details
		/// </summary>
		/// <param name="order">object with updated information</param>
		/// <returns></returns>
		Task<Order> UpdateAsync(Order order);
	}
}
