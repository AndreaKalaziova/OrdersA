using System.ComponentModel.DataAnnotations;

namespace Orders.Api.Models
{
	/// <summary>
	/// DTO for updating payment info that will change Order Sttaus (paid = 'paid' / not-paid='cancelled'
	/// </summary>
	public class PaymentInfoDTO
	{
		[Required]
		public uint OrderNumber { get; set; }

		[Required]
		public bool IsPaid { get; set; } // true = paid, false = not paid
	}

}
