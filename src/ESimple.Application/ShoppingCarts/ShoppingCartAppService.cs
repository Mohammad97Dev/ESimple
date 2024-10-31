using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using AutoMapper;
using ESimple.Authorization;
using ESimple.Domains.Products;
using ESimple.Domains.ShoppingCartItems;
using ESimple.Domains.ShoppingCarts;
using ESimple.ShoppingCarts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.ShoppingCarts
{
    [AbpAuthorize(PermissionNames.ShoppingCart_FullControl)]
    public class ShoppingCartAppService : ApplicationService, IShoppingCartAppService
    {
        private readonly IShoppingCartManager _shoppingCartManager;
        private readonly IShoppingCartItemManager _shoppingCartItemManager;
        private readonly IProductManager _productManager;

        public ShoppingCartAppService(
            IShoppingCartManager shoppingCartManager,
            IShoppingCartItemManager shoppingCartItemManager,
            IProductManager productManager)
        {
            _shoppingCartManager = shoppingCartManager;
            _shoppingCartItemManager = shoppingCartItemManager;
            _productManager = productManager;
        }

        public async Task<List<ShoppingCartDto>> GetCartsByUserIdAsync()
        {
            var carts = await _shoppingCartManager.GetCartsByUserIdAsync(AbpSession.UserId.Value);
            return ObjectMapper.Map<List<ShoppingCartDto>>(carts);
        }
        public async Task<ShoppingCartDto> GetCartByIdAsync(int cartId)
        {
            try
            {
                var carts = await _shoppingCartManager.GetCartByIdAsync(cartId);
                return ObjectMapper.Map<ShoppingCartDto>(carts);
            }
            catch (Exception ex)
            { throw; }
        }

        public async Task AddOrUpdateItemAsync(int cartId, CreateShoppingCartItemDto input)
        {
            var product = await _productManager.GetProductByIdAsync(input.ProductId);
            if (product == null || product.Stock < input.Quantity)
            {
                throw new UserFriendlyException("Insufficient stock for product.");
            }

            await _shoppingCartItemManager.AddOrUpdateItemAsync(cartId, input.ProductId, input.Quantity);
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            await _shoppingCartItemManager.RemoveItemAsync(cartItemId);
        }

        public async Task UpdateItemQuantityAsync(int cartItemId, int quantity)
        {
            await _shoppingCartItemManager.UpdateItemQuantityAsync(cartItemId, quantity);
        }

        public async Task ClearCartAsync(int cartId)
        {
            await _shoppingCartManager.ClearCartAsync(cartId);
        }

        public async Task DeleteCartAsync(int cartId)
        {
            await _shoppingCartManager.DeleteCartAsync(cartId);
        }

        public async Task CreateCartAsync(CreateShoppingCartItemDto input)
        {
            var cart = new ShoppingCart
            {
                UserId = AbpSession.UserId.Value,
            };
            var product = await _productManager.GetProductByIdAsync(input.ProductId);
            if (product == null || product.Stock < input.Quantity)
            {
                throw new UserFriendlyException("Insufficient stock for product.");
            }
            cart.Items.Add(new ShoppingCartItem
            {
                ProductId = input.ProductId,
                Price = product.Price,
                Quantity = input.Quantity,
            });
            await _shoppingCartManager.CreateCartAsync(cart);

        }
    }
}
