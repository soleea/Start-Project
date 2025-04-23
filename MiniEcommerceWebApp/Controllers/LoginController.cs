using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.Core.DTO.Customer;
using MiniEcommerceWebApi.Core.Model;

namespace MiniEcommerceWebApi.Controllers
{

    public class LoginController : EcommerceControllerBase<CustomerDTO, Customer, ILoginBusinessService>
    {
        public LoginController(ILogger<Customer> logger
           , ILoginBusinessService businessService
           , IDateTimeService dateTimeBusinessServices
           , IMapper mapper
           , IConfiguration configuration
           , IServiceScopeFactory scopeFactory)
           : base(logger, businessService, dateTimeBusinessServices, mapper, configuration, scopeFactory)
        {
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO item)
        {
            try
            {
              
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid CreateLogin State");
                }

                var result = MapDTOToEntityWithNoID<LoginDTO, Customer>(item);
                SetAuditInformation(result);

                await BusinessServiceManager.LoginAsync(result);

                return Ok(result.Id);

            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(LoginAsync)} of {nameof(LoginController)}");
                return StatusCode(500);
            }
        }


       
    }
}