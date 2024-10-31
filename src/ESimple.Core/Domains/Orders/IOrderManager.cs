using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Domains.Orders
{
    public interface IOrderManager : IDomainService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task DeleteOrderAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(long userId);
    }
}
