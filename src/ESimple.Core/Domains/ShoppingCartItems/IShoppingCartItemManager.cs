using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.ShoppingCartItems
{
    public interface IShoppingCartItemManager : IDomainService
    {
        Task AddOrUpdateItemAsync(int cartId, int productId, int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task UpdateItemQuantityAsync(int cartItemId, int quantity);
    }
}
