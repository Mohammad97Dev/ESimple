using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.Orders
{
    public class OrderManager : DomainService, IOrderManager
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderManager(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await _orderRepository.InsertAsync(order);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetAll()
                .AsNoTracking()
                .Include(c => c.OrderItems)
                .ThenInclude(c => c.Product)
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync() ??
                throw new EntityNotFoundException(typeof(Order), orderId);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteAsync(orderId);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(long userId)
        {
            return await _orderRepository.GetAllListAsync(x => x.UserId == userId);
        }
    }

}
