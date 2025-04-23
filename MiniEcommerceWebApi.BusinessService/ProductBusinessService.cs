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

namespace MiniEcommerceWebApi.BusinessService
{

    public class ProductBusinessService : BusinessServiceBase<Product, IProductRepository>
      , IProductBusinessService
    {

        public IConfiguration Configuration { get; }

        public ProductBusinessService(IProductRepository repository
            , IDateTimeService dateTimeBusinessServices
            , IConfiguration configuration
            , ILogger<Product> logger
            , IServiceScopeFactory scopeFactory)
            : base(repository, dateTimeBusinessServices, logger, scopeFactory)
        {
            Configuration = configuration;

        }

    }
}