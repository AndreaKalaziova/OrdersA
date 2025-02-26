﻿using Microsoft.AspNetCore.Mvc;
using Orders.Api.Interfaces;
using Orders.Api.Models;

namespace Orders.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderManager orderManager;

		public OrdersController(IOrderManager orderManager)
		{
			this.orderManager = orderManager;
		}

		/// <summary>
		/// get all orders
		/// </summary>
		/// <returns>list of orders</returns>
		[HttpGet]
		public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
		{
			return await orderManager.GetAllOrdersAsync();
		}

		/// <summary>
		/// add new order to db, input with OrderItems -> return their details
		/// </summary>
		/// <param name="orderDTO">order data to be added</param>
		/// <returns>newly created order with its details as orderDTO</returns>
		[HttpPost]
		public IActionResult AddOrder([FromBody] OrderDTO order)
		{ 
			//check if OrderDTO inserted as per requirements
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				// atempot to add the order
				OrderDTO? createdOrder = orderManager.AddOrder(order);
				return StatusCode(StatusCodes.Status201Created, createdOrder);
			}
			catch (InvalidOperationException ex)
			{
				//return a conflict response if OrderNumber already used
				return Conflict(new { message = ex.Message });
			}
		}

		/// <summary>
		/// for recieving information about payment -> that will upadte order status accordingly
		/// </summary>
		/// <param name="paymentInfo">containing OrderNumber and IsPaid (true = paid/false=not paid)</param>
		/// <returns></returns>
		[HttpPost("update-order-status")]
		public async Task<IActionResult> UpdateOrderStatus([FromBody] PaymentInfoDTO paymentInfo)
		{
			//checks if payment info provided
			if (paymentInfo == null)
				return BadRequest("Informace o platbe musi byt zadano (true=zaplaceno / false=nezaplaceno).");

			try
			{
				//updates the order state based on payment info (true = paid/false=not paid)
				var updatedOrder = await orderManager.UpdateOrderStateAsync(paymentInfo);
				return Ok(updatedOrder);		//return updated order details
			}
			catch (InvalidOperationException ex)
			{
				//retunr not found if the order does not exists
				return NotFound(ex.Message);
			}
		}
	}
}
