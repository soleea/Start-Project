using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniEcommerceWebApi.Core.Model;
using MiniEcommerceWebApi.Repository.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration, ILogger<Product> logger, IServiceScopeFactory scopeFactory)
            : base(configuration, logger, scopeFactory)
        {
        }

    }
}
