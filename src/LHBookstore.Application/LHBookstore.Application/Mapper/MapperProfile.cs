using AutoMapper;
using LHBookstore.Application.DTOs.Book;
using LHBookstore.Application.DTOs.Order;
using LHBookstore.Domain.Entities;

namespace LHBookstore.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BookRequestDto, Book>();
            CreateMap<Book, BookResponseDto>();
            CreateMap<OrderRequestDto, Order>();
            CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.OrderItems.Sum(item => item.Quantity)))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)); // Add this line
            CreateMap<OrderItem, OrderItemResponseDto>();
        }
    }
}
