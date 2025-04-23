using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MiniEcommerceWebApi.Core.Model;
using MiniEcommerceWebApi.Repository.Interface;

namespace MiniEcommerceWebApi.Repository
{

    public class LoginRepository : RepositoryBase<Customer>, ILoginRepository
    {
        public LoginRepository(IConfiguration configuration, ILogger<Customer> logger, IServiceScopeFactory scopeFactory)
            : base(configuration, logger, scopeFactory)
        {
        }

        public async Task<bool> Login(Customer customer)
        {
            try
            {
                using (var scope = ScopeFactory.CreateScope())
                {
                    using (var databaseContext = scope.ServiceProvider
                                                   .GetRequiredService<EcommerceDbContext>())
                    {
                       
                        var entity = await databaseContext.Set<Customer>()
                                              .Where(x => x.Email.Equals(customer.Email) && x.Password.Equals(customer.Password))
                                              .SingleOrDefaultAsync();

                        var typeName = nameof(Customer);

                        HealthLogger.LogInformation($" Successfully retrieved {typeName} with the Id: '{entity?.Id} ");

                        if (entity != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, " error at LoginAsync with page number", customer.Email);

                throw;
            }
        }

    }
}