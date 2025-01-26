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
			CreateMap<OrderItemInsertDTO, OrderItem>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

			// Mapping for OrderInsertDTO to Order
			CreateMap<OrderInsertDTO, Order>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

			// OrderItem to OrderItemDTO mapping including Product info
			CreateMap<OrderItem, OrderItemDTO>()
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
				.ReverseMap();
				//.ForMember(dest => dest.Product, opt => opt.Ignore()); // Handle Product separately (fetching from db)

			// manual mapping for Order to OrderDTO  with OrderItems
			CreateMap<Order, OrderDTO>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

			// manual mapping for OrderDTO to Order with OrderItems
			CreateMap<OrderDTO, Order>()
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

		}
	}
}
