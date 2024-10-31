using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.ShoppingCarts.Dto
{
    public class ShoppingCartDto : EntityDto<int>
    {
        public long UserId { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; } = new List<ShoppingCartItemDto>();
    }
}
