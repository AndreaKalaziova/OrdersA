
namespace Orders.Data.Models
{
	/// <summary>
	/// item on order consisting of Product and its details + quantity
	/// </summary>
	public class OrderItem
	{
		/// <summary>
		/// Unique id of the OrderItem
		/// </summary>
		public uint OrderItemId { get; set; }

		public uint ProductId { get; set; }
		/// <summary>
		/// product and its details that belongs to this Order Item
		/// </summary>
		/// / no need to inic. EF can do it
		public Product Product { get; set; }
		/// <summary>
		/// count of product in the order
		/// </summary>
		public uint Quantity { get; set; }
		/// <summary>
		/// foreign key, id of the order the which the product belongs to
		/// </summary>
		public uint OrderId { get; set; }
		/// <summary>
		/// navigation propersty for the order the which the Order Item belongs to
		/// </summary>
		public Order Order { get; set; }    //navigation property
	}
}
