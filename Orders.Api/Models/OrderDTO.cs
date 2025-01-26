using Orders.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for order including State that changes per payment status, including OrdeLines/ products on Order
	/// </summary>
	public class OrderDTO
	{
		/// <summary>
		/// order number
		/// </summary>
		// chceck for duplicity OrderNumber - in orderManager
		[JsonPropertyName("Číslo objednávky")]
		public uint OrderNumber { get; set; }
		/// <summary>
		/// name of customer or company 
		/// </summary>
		[MaxLength(100, ErrorMessage = "Název může mít maximálaně 100 znaků.")]
		[JsonPropertyName("Jméno zákazníka nebo název firmy")]
		public string CustomerName { get; set; } = "";
		/// <summary>
		/// date of issue of the order, format yyyy-mm-dd
		/// </summary>
		[JsonPropertyName("Datum vytvoření objednávky")]
		public DateTime Issued { get; set; }
		/// <summary>
		/// state of order - new (by default on creation) / payed / cancelled
		/// </summary>
		[JsonPropertyName("Stav objednávky")]
		public OrderState State { get; set; }
		/// <summary>
		/// items on the order
		/// </summary>
		[MinLength(1, ErrorMessage = "Alespoň jedna položka musí být uvedena.")]
		[JsonPropertyName("Položky objednávky")]
		public IList<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>(); // inic. as empty list, so it is not null by default
	}
}
