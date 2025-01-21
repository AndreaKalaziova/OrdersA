using Orders.Api.Models;

namespace Orders.Api.Interfaces
{
	public interface IOrderManager
	{
		OrderDTO AddOrder(OrderDTO orderDTO);
		Task <IList<OrderDTO>> GetAllOrdersAsync();
		Task<OrderDTO> UpdateOrderStateAsync(PaymentInfoDTO paymentInfo);
	}
}
