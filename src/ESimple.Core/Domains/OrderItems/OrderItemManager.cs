using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.OrderItems
{
    public class OrderItemManager : DomainService, IOrderItemManager
    {
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderItemManager(IRepository<OrderItem> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            return await _orderItemRepository.InsertAsync(orderItem);
        }

        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            return await _orderItemRepository.UpdateAsync(orderItem);
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            await _orderItemRepository.DeleteAsync(orderItemId);
        }
    }

}
