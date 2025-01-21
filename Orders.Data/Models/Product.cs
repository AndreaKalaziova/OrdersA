
namespace Orders.Data.Models
{
	public class Product
	{
		/// <summary>
		/// Unique id of the product
		/// </summary>
		public uint ProductId { get; set; }
		/// <summary>
		/// name of the product
		/// </summary>
		public string Name { get; set; } = "";
		/// <summary>
		/// count of product in the order
		/// </summary>
		public uint Quantity { get; set; }
		/// <summary>
		/// price of the product per 1 piece
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// foreign key, id of the order the which the product belongs to
		/// </summary>
		public uint OrderId { get; set; }
		/// <summary>
		/// navigation propersty for the order the which the product belongs to
		/// </summary>
		public Order Order { get; set; }	//navigation property
	}
}
