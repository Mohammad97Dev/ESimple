using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.UI;
using AutoMapper;
using ESimple.Domains.OrderItems;
using ESimple.Domains.Orders;
using ESimple.Domains.Products;
using ESimple.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Net.Mail;
using ESimple.EmailSenders;
using ESimple.Sessions;
using Abp.Authorization;
using ESimple.Authorization;

namespace ESimple.Orders
{
    [AbpAuthorize]
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderItemManager _orderItemManager;
        private readonly IProductManager _productManager;
        private readonly IEmailSenderAppService _emailSenderAppService;
        private readonly ISessionAppService _sessionAppService;

        public OrderAppService(IOrderManager orderManager, IOrderItemManager orderItemManager, IProductManager productManager, IEmailSenderAppService emailSenderAppService, ISessionAppService sessionAppService)
        {
            _orderManager = orderManager;
            _orderItemManager = orderItemManager;
            _productManager = productManager;
            _emailSenderAppService = emailSenderAppService;
            _sessionAppService = sessionAppService;
        }
        [AbpAuthorize(PermissionNames.Order_Create)]
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            try
            {
                var order = new Order
                {
                    UserId = AbpSession.UserId.Value,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = 0 // Initial amount, will be calculated below
                };
                var products = await _productManager.CheckAndGetListOfProduct(input.Items.Select(x => x.ProductId).ToList());
                foreach (var item in input.Items)
                {
                    var product = products.First(x => x.Id == item.ProductId);
                    if (product.Stock < item.Quantity)
                    {
                        throw new UserFriendlyException($"Product {item.ProductId} does not have sufficient stock.");
                    }

                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price
                    };

                    order.TotalAmount += product.Price * item.Quantity;
                    order.OrderItems.Add(orderItem);
                }

                var createdOrder = await _orderManager.CreateOrderAsync(order);
                var email = (await _sessionAppService.GetCurrentLoginInformations()).User.EmailAddress;
                await _emailSenderAppService.SendEmail(email);
                return ObjectMapper.Map<OrderDto>(createdOrder);
            }
            catch (Exception ex) { throw; }
        }
        [AbpAuthorize(PermissionNames.Order_Get)]
        public async Task<OrderDto> GetAsync(EntityDto<int> input)
        {
            var order = await _orderManager.GetOrderByIdAsync(input.Id);
            return ObjectMapper.Map<OrderDto>(order);
        }
        [AbpAuthorize(PermissionNames.Order_List)]
        public async Task<ListResultDto<OrderDto>> GetAllAsync()
        {
            var orders = await _orderManager.GetOrdersByUserIdAsync(AbpSession.UserId.Value);
            return new ListResultDto<OrderDto>(ObjectMapper.Map<List<OrderDto>>(orders));
        }
        [AbpAuthorize(PermissionNames.Order_Delete)]
        public async Task DeleteAsync(EntityDto<int> input)
        {
            await _orderManager.DeleteOrderAsync(input.Id);
        }
    }
}
