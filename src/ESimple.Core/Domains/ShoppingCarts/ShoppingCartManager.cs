using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using ESimple.Domains.ShoppingCartItems;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.ShoppingCarts
{
    public class ShoppingCartManager : DomainService, IShoppingCartManager
    {
        private readonly IRepository<ShoppingCart, int> _shoppingCartRepository;
        private readonly IRepository<ShoppingCartItem, int> _shoppingCartItemRepository;

        public ShoppingCartManager(
            IRepository<ShoppingCart, int> shoppingCartRepository,
            IRepository<ShoppingCartItem, int> shoppingCartItemRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task<List<ShoppingCart>> GetCartsByUserIdAsync(long userId)
        {
            return await _shoppingCartRepository
                .GetAll()
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<ShoppingCart> CreateCartAsync(ShoppingCart input)
        {
            input = await _shoppingCartRepository.InsertAsync(input);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return input;
        }

        public async Task ClearCartAsync(int cartId)
        {
            var items = await _shoppingCartItemRepository.GetAllListAsync(i => i.ShoppingCartId == cartId);
            foreach (var item in items)
            {
                await _shoppingCartItemRepository.DeleteAsync(item);
            }
        }

        public async Task DeleteCartAsync(int cartId)
        {
            await ClearCartAsync(cartId);
            await _shoppingCartRepository.DeleteAsync(cartId);
        }

        public async Task<ShoppingCart> GetCartByIdAsync(int id)
        {
            return await _shoppingCartRepository.GetAll()
                 .AsNoTracking()
                 .Include(c => c.Items)
                 .ThenInclude(c => c.Product)
                 .Where(x => x.Id == id)
                 .FirstOrDefaultAsync() ??
                 throw new EntityNotFoundException(typeof(ShoppingCart), id);
        }
    }

}
