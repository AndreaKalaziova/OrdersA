using Orders.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for OrderItem with product (and its details) + quantity
	/// </summary>
	public class OrderItemDTO : ProductDTO
	{
		/// <summary>
		/// count of product in the order
		/// </summary>
		[JsonPropertyName("Počet kusů zboží")]
		public uint Quantity { get; set; }
	}
}
