using Abp.Application.Services.Dto;
using ESimple.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.ShoppingCarts.Dto
{
    public class ShoppingCartItemDto : EntityDto<int>
    {
        public ProductDto Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
