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
            CreateMap<Order, OrderResponseDto>();
        }
    }
}
