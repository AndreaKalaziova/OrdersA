using Orders.Api.Models;

namespace Orders.Api.Interfaces
{
	public interface IOrderManager
	{
		Task<OrderDTO> AddOrderAsync(OrderInsertDTO orderInsertDTO);
		Task <IList<OrderDTO>> GetAllOrdersAsync();
		Task<OrderDTO> UpdateOrderStateAsync(PaymentInfoDTO paymentInfo);
	}
}
