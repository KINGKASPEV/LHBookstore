﻿using LHBookstore.Domain.Enums;

namespace LHBookstore.Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
