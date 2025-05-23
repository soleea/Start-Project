﻿using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.Core.DTO.Product;
using MiniEcommerceWebApi.Core.Interface;
using MiniEcommerceWebApi.Core.Model;

using Newtonsoft.Json;

namespace MiniEcommerceWebApi.Controllers
{
   
  public class ProductController : EcommerceControllerBase<ProductDTO, Product, IProductBusinessService>
    {
        public ProductController(ILogger<Product> logger
           , IProductBusinessService businessService
           , IDateTimeService dateTimeBusinessServices
           , IMapper mapper
           , IConfiguration configuration
           , IServiceScopeFactory scopeFactory)
           : base(logger, businessService, dateTimeBusinessServices, mapper, configuration, scopeFactory)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateProductDTO item)
        {
            try
            {
                if (item != null)
                {
                    var requestBody = $"create  Request Body:  {JsonConvert.SerializeObject(item)}";
                    HealthLogger.LogInformation(requestBody);
                }

                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid CreateUserRecord State");
                }

                var result = MapDTOToEntityWithNoID<CreateProductDTO, Product>(item);
                SetAuditInformation(result);

                await BusinessServiceManager.AddAsync(result);

                return Ok(result.Id);

            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(AddAsync)} of {nameof(ProductController)}");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductDTO item, int id)
        {
            try
            {
                var updateStudent = MapperManager.Map<UpdateProductDTO, ProductDTO>(item);
                return await base.UpdateAsync(updateStudent, id);
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(UpdateAsync)} of {nameof(ProductController)}");
                return StatusCode(500);
            }
        }

       

        [HttpGet()]
        public new async Task<IActionResult> ListAsync(int pageNo)
        {
            try
            {
                return await base.ListAsync(pageNo);
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(ListAsync)} of {nameof(ProductController)}");
                return StatusCode(500);
            }
        }


        protected override Product SetUpdateAuditInformation(ProductDTO TDTO, Product entity)
        {
            // Store the created by audit information
            var createdBy = entity.CreatedBy;
            var createdOn = entity.CreatedDate;

            //set all properties on db entity  into copy as back up
            Product copy = SetAllDbValuesIntoACopy(entity);

            MapperManager.Map(TDTO, entity);

            //set non updateble properties back into entity
            SetNonUpdatableDbEntityPropertiesFromCopy(entity, copy);

            //replace possible null value from  update dto with db values
            ReplaceNullOnDtoWithDbValues(entity, copy);

            // Set the created by audit information as it may be lost during mapping
            entity.CreatedBy = createdBy;
            entity.CreatedDate = createdOn;

            return entity;
        }

        private void ReplaceNullOnDtoWithDbValues(Product entity, Product dbCopy)
        {
            
            entity.ProductName = entity.ProductName ?? dbCopy.ProductName;
          
        }

        private void SetNonUpdatableDbEntityPropertiesFromCopy(Product entity, Product dbCopy)
        {
            entity.Id = dbCopy.Id;
           
            entity.Description = dbCopy.Description;
            entity.UnitPrice = dbCopy.UnitPrice;
        }
        private Product SetAllDbValuesIntoACopy(Product entity)
        {
            var copy = new Product();

            copy.Id = entity.Id;
            copy.ProductName=entity.ProductName;
            copy.Description = entity.Description;
            copy.UnitPrice = entity.UnitPrice;
            return copy;
        }
    }
}
