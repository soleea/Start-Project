using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.Core.DTO.Order;
using MiniEcommerceWebApi.Core.Model;

namespace MiniEcommerceWebApi.Controllers
{

    public class OrderController : EcommerceControllerBase<OrderDTO, Order, IOrderBusinessService>
    {
        public OrderController(ILogger<Order> logger
           , IOrderBusinessService businessService
           , IDateTimeService dateTimeBusinessServices
           , IMapper mapper
           , IConfiguration configuration
           , IServiceScopeFactory scopeFactory)
           : base(logger, businessService, dateTimeBusinessServices, mapper, configuration, scopeFactory)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderDTO item)
        {
            try
            {
              
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid CreateOrder State");
                }

                var result = MapDTOToEntityWithNoID<CreateOrderDTO, Order>(item); 

                SetAuditInformation(result);

                await BusinessServiceManager.AddAsync(result);

                return Ok(result.Id);

            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(AddAsync)} of {nameof(OrderController)}");
                return StatusCode(500);
            }
        }


       
    }
}