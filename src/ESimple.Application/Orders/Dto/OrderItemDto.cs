using Abp.Application.Services.Dto;
using ESimple.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Orders.Dto
{
    public class OrderItemDto : EntityDto<int>
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
