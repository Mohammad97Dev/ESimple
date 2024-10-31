using AutoMapper;
using ESimple.Domains.ShoppingCartItems;
using ESimple.Domains.ShoppingCarts;
using ESimple.ShoppingCarts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.ShoppingCarts.Mapper
{
    public class ShoppingCartMapProfile : Profile
    {
        public ShoppingCartMapProfile()
        {
            CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
        }
    }
}
