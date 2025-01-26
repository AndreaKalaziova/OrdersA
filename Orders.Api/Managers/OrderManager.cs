using AutoMapper;
using Orders.Api.Interfaces;
using Orders.Api.Models;
using Orders.Data.Interfaces;
using Orders.Data.Models;

namespace Orders.Api.Managers
{
	public class OrderManager : IOrderManager
	{
		private readonly IOrderRepository orderRepository;  // repository to manage db operations for Order entities
		private readonly IMapper mapper;                    // automapper for mapping between entity and Dto objects

		public OrderManager(IOrderRepository orderRepository, IMapper mapper)
		{
			this.orderRepository = orderRepository;			//DI for Order repository
			this.mapper = mapper;							//DI for Autmapper
		}

		/// <summary>
		/// get all orders, async
		/// </summary>
		/// <returns>list of orders</returns>
		public async Task<IList<OrderDTO>> GetAllOrdersAsync()
		{
			IList<Order> orders = await orderRepository.GetAllAsync();	//get all orders from db
			return mapper.Map<IList<OrderDTO>>(orders);		//return list of orders mapped to OrderDto
		}

		/// <summary>
		/// add new order into db
		/// </summary>
		/// <param name="orderDTO">order data to be added</param>
		/// <returns>newly created order with its details as orderDto</returns>
		public async Task<OrderDTO> AddOrderAsync(OrderInsertDTO orderInsertDTO)
		{
			// Check if an order with the same OrderNumber already exists
			if (orderRepository.ExistsWithOrderNumber(orderInsertDTO.OrderNumber))
				throw new InvalidOperationException($"Čislo objednavky {orderInsertDTO.OrderNumber} je již použito.");

			Order order = mapper.Map<Order>(orderInsertDTO);  //mapping inserted orderDTO to the Order entity
			order.State = OrderState.New;				//new order is inserted with status New
			order.OrderId = default;                    // reset the orderid to default for auto-generation in the db

			order = await orderRepository.InsertAsync(order);
			Order addedOrder = await orderRepository.GetOrderByNumberAsync(order.OrderNumber); // new order added into repository

			return mapper.Map<OrderDTO>(addedOrder);	//inserted order entity mapped back to orderDTO for return
		}

		/// <summary>
		///  updates the state of an order based on payment status (paid ='paid' / not paid ='cancelled')
		/// </summary>
		/// <param name="orderNumber"></param>
		/// <param name="isPaid"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public async Task<OrderDTO> UpdateOrderStateAsync(PaymentInfoDTO paymentInfo)
		{
			// find order by OrderNumber
			var order = await orderRepository.GetOrderByNumberAsync(paymentInfo.OrderNumber);
			if (order == null)
				throw new InvalidOperationException($"Objednavka s cislem {paymentInfo.OrderNumber} nenalezena.");

			order.State = paymentInfo.IsPaid ? OrderState.Paid : OrderState.Cancelled; // Update the state based on IsPaid (paid ='paid' / not paid ='cancelled')
			await orderRepository.UpdateAsync(order);       // Save the changes
			return mapper.Map<OrderDTO>(order);             // Map and return the updated order
		}
	}
}
