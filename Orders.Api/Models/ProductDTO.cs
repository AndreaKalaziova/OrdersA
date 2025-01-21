using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for product = OrderLine
	/// </summary>
	public class ProductDTO
	{
		/// <summary>
		/// name of the product
		/// </summary>
		[MaxLength(100, ErrorMessage = "Název může mít maximálaně 100 znaků.")]
		[JsonPropertyName("Název zboží")]
		public string Name { get; set; } = "";
		/// <summary>
		/// count of product in the order
		/// </summary>
		[JsonPropertyName("Počet kusů zboží")]
		public uint Quantity { get; set; }
		/// <summary>
		/// price of the product 
		/// </summary>
		[Range(0.01, double.MaxValue, ErrorMessage = "Cena musí být zadaná jako kladné číslo.")]
		[JsonPropertyName("Cenu za jeden kus zboží")]
		public decimal Price { get; set; }
	}
}
