using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.ShoppingCarts
{
    public interface IShoppingCartManager : IDomainService
    {
        Task<List<ShoppingCart>> GetCartsByUserIdAsync(long userId);
        Task<ShoppingCart> CreateCartAsync(ShoppingCart input);
        Task ClearCartAsync(int cartId);
        Task DeleteCartAsync(int cartId);
        Task<ShoppingCart> GetCartByIdAsync(int id);
    }

}
