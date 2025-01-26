
namespace Orders.Data.Models
{
	/// <summary>
	/// product with is detials (name, price per 1piece)
	/// </summary>
	public class Product
	{
		/// <summary>
		/// Unique id of the product
		/// </summary>
		public uint ProductId { get; set; }
		/// <summary>
		/// name of the product
		/// </summary>
		public string ProductName { get; set; } = "";
		/// <summary>
		/// price of the product per 1 piece
		/// </summary>
		public decimal Price { get; set; }
	}
}
