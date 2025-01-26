using System.Text.Json.Serialization;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for OrderItem on Inserting new Order - where only productId and qunatity are mentioned
	/// </summary>
	public class OrderItemInsertDTO
	{
		public uint ProductId { get; set; }

		[JsonPropertyName("Počet kusů zboží")]
		public uint Quantity { get; set; }
	}
}
