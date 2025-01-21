using AutoMapper;
using Orders.Api.Models;
using Orders.Data.Models;

namespace Orders.Api
{
	/// <summary>
	/// Automapper configuration to define mapping between entities and DTOs
	/// </summary>
	public class AutomapperConfigurationProfile : Profile
	{
		public AutomapperConfigurationProfile()
		{
			// two-way mapping between Product and ProductDTO
			CreateMap<Product, ProductDTO>().ReverseMap();

			// manual mapping for Order to OrderDTO  with OrderItems
			CreateMap<Order, OrderDTO>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

			// manual mapping for OrderDTO to Order with OrderItems
			CreateMap<OrderDTO, Order>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

		}
	}
}
