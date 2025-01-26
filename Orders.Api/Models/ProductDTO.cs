using System.Text.Json.Serialization;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for product and its details
	/// </summary>
	public class ProductDTO
	{
		[JsonIgnore]	//keeps in DTO for mapping, but does not show (not being serialized) in json
		public uint ProductId { get; set; }

		/// <summary>
		/// name of the product
		/// </summary>
		[JsonPropertyName("Název zboží")]
		public string ProductName { get; set; } = "";

		/// <summary>
		/// price of the product 
		/// </summary>
		[JsonPropertyName("Cenu za jeden kus zboží")]
		public decimal Price { get; set; }
	}
}
