using MiniEcommerceWebApi.BusinessService;
using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.BusinessService.Utilities;
using MiniEcommerceWebApi.Repository;
using MiniEcommerceWebApi.Repository.Interface;

namespace MiniEcommerceWebApi
{
    public class ClientApiDependency
    {
        public static void Resgister(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IDateTimeService, DateTimeService>();

            builder.Services.AddScoped<ILoginBusinessService, LoginBusinessService>();
            builder.Services.AddScoped<IOrderBusinessService, OrderBusinessService>();
            builder.Services.AddScoped<IProductBusinessService, ProductBusinessService>();
        }
    }
}
