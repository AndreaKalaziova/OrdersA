﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Orders.Data.Models
{
	/// <summary>
	/// state of the order : 0=new, 1= payed, 2=cancelled
	/// </summary>
	public enum OrderState
	{ New, Paid, Cancelled }

	/// <summary>
	/// order's details
	/// </summary>
	public class Order
	{
		/// <summary>
		/// unique identification key generated by db
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
		public uint OrderId {  get; set; }
		/// <summary>
		/// order number (required)
		/// </summary>
		[Required]
		public uint OrderNumber { get; set; }
		/// <summary>
		/// name of customer or company (required)
		/// </summary>
		[Required, MinLength(2)]
		public string CustomerName { get; set; } = "";
		/// <summary>
		/// date of issue of the order (required)
		/// </summary>
		[Required]
		public DateTime Issued { get; set; }
		/// <summary>
		/// state of order - new / paid / cancelled - at creation status is "new", once paid it is "paid", if not paid it is "cancelled" (required)
		/// </summary>
		[Required]
		public OrderState State { get; set; }
		/// <summary>
		/// items on the order (required)
		/// </summary>
		[Required, MinLength(1, ErrorMessage = "Alespoň jedna položka musí být uvedena.")]
		public IList<Product> OrderItems { get; set; } = new List<Product>(); // inic. as empty list, so it is not null by default
	}
}
