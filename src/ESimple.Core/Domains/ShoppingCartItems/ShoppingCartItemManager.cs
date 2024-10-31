using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using ESimple.Domains.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.ShoppingCartItems
{
    public class ShoppingCartItemManager : DomainService, IShoppingCartItemManager
    {
        private readonly IRepository<ShoppingCartItem, int> _shoppingCartItemRepository;
        private readonly IRepository<Product, int> _productRepository;

        public ShoppingCartItemManager(
            IRepository<ShoppingCartItem, int> shoppingCartItemRepository,
            IRepository<Product, int> productRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
        }

        public async Task AddOrUpdateItemAsync(int cartId, int productId, int quantity)
        {
            var product = await _productRepository.FirstOrDefaultAsync(productId);
            if (product == null || product.Stock < quantity)
            {
                throw new UserFriendlyException("Insufficient stock for product.");
            }

            var cartItem = await _shoppingCartItemRepository.FirstOrDefaultAsync(i => i.ShoppingCartId == cartId && i.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                await _shoppingCartItemRepository.UpdateAsync(cartItem);
                await UnitOfWorkManager.Current.SaveChangesAsync();
            }
            else
            {
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cartId,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                await _shoppingCartItemRepository.InsertAsync(cartItem);
            }
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            await _shoppingCartItemRepository.DeleteAsync(cartItemId);
        }

        public async Task UpdateItemQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _shoppingCartItemRepository.GetAsync(cartItemId);
            cartItem.Quantity = quantity;
            await _shoppingCartItemRepository.UpdateAsync(cartItem);
        }
    }
}
