using Abp.Application.Services;
using ESimple.ShoppingCarts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.ShoppingCarts
{
    public interface IShoppingCartAppService : IApplicationService
    {
        Task<List<ShoppingCartDto>> GetCartsByUserIdAsync();
        Task AddOrUpdateItemAsync(int cartId, CreateShoppingCartItemDto input);
        Task RemoveItemAsync(int cartItemId);
        Task UpdateItemQuantityAsync(int cartItemId, int quantity);
        Task ClearCartAsync(int cartId);
        Task DeleteCartAsync(int cartId);
        Task CreateCartAsync(CreateShoppingCartItemDto input);
    }
}
