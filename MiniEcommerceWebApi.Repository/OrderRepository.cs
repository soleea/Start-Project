using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MiniEcommerceWebApi.Core.Model;
using MiniEcommerceWebApi.Repository.Interface;

namespace MiniEcommerceWebApi.Repository
{

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IConfiguration configuration, ILogger<Order> logger, IServiceScopeFactory scopeFactory)
            : base(configuration, logger, scopeFactory)
        {
        }

    }
}
