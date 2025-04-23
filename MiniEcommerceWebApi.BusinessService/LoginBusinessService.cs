using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.Core.Model;
using MiniEcommerceWebApi.Repository.Interface;

namespace MiniEcommerceWebApi.BusinessService
{

    public class LoginBusinessService : BusinessServiceBase<Customer, ILoginRepository>
      , ILoginBusinessService
    {

        public IConfiguration Configuration { get; }

        public LoginBusinessService(ILoginRepository repository
            , IDateTimeService dateTimeBusinessServices
            , IConfiguration configuration
            , ILogger<Customer> logger
            , IServiceScopeFactory scopeFactory)
            : base(repository, dateTimeBusinessServices, logger, scopeFactory)
        {
            Configuration = configuration;

        }

       
        public async Task<bool> LoginAsync(Customer customer)
        {

            if (customer != null)
            {

                //    var claims = new List<Claim>
                //    {
                //        new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                //        new Claim(ClaimTypes.Name, customer.CustomerName),
                //        new Claim(ClaimTypes.Email, customer.Email)
                //    };

                //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await RepositoryManager.Login(customer);
                return true;
            }
            else
            {
                 await RepositoryManager.Login(customer);
                return false;
                
            }
                
        }
    }
}