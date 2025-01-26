using System.Text.Json.Serialization;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for adding new Order - where State is set to New by default and OrderLines include only ProductId + quantity
	/// </summary>
	public class OrderInsertDTO
	{
		[JsonPropertyName("Číslo objednávky")]
		public uint OrderNumber { get; set; }

		[JsonPropertyName("Jméno zákazníka nebo název firmy")]
		public string CustomerName { get; set; } = "";

		[JsonPropertyName("Datum vytvoření objednávky")]
		public DateTime Issued { get; set; }

		[JsonPropertyName("Položky objednávky")]
		public IList<OrderItemInsertDTO> OrderItems { get; set; } = new List<OrderItemInsertDTO>();
	}
}
