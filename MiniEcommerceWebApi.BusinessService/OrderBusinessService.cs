using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.Core.Model;
using MiniEcommerceWebApi.Repository.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniEcommerceWebApi.BusinessService
{
 
    public class OrderBusinessService : BusinessServiceBase<Order, IOrderRepository>
      , IOrderBusinessService
    {

        public IConfiguration Configuration { get; }
        private readonly IProductBusinessService productBusinessService;

        public OrderBusinessService(IOrderRepository repository
            , IDateTimeService dateTimeBusinessServices
            , IProductBusinessService productBusinessService
            , IConfiguration configuration
            , ILogger<Order> logger
            , IServiceScopeFactory scopeFactory)
            : base(repository, dateTimeBusinessServices, logger, scopeFactory)
        {
            Configuration = configuration;
            this.productBusinessService= productBusinessService;

        }


        public override async Task<int> AddAsync(Order order)
        {

            CheckIfNull(order);
            CheckIfAddedEntityHasId(order.Id);

            var orderNumber = $"ORD-{DateTime.UtcNow.Ticks}";
            
            var myOrder = new Order
            {
                CustomerId = order.CustomerId,
                OrderNumber = orderNumber,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending", 
                CreatedDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;
           
            foreach (var item in order.OrderItems)
            {
                
                if (item.Quantity <= 0)
                    continue;
               
                var product = await productBusinessService.GetAsync(item.ProductId);

                if (product == null)
                    continue;

               
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.UnitPrice
                };
               
                totalAmount += orderItem.Total;
               
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
          
            return await RepositoryManager.AddAsync(order);

        }
    }
}