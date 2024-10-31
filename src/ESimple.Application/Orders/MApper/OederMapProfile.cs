using AutoMapper;
using ESimple.Domains.OrderItems;
using ESimple.Domains.Orders;
using ESimple.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Orders.MApper
{
    public class OederMapProfile : Profile
    {
        public OederMapProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        }
    }
}
