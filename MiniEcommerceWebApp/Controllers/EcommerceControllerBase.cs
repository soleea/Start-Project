using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.Core.Interface;

namespace MiniEcommerceWebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class EcommerceControllerBase<TDTO, TEntity, TBusinessServiceManager>
      : ControllerBase
      where TEntity : class, IEntity, IEntityIdentity, new()
      where TDTO : class, IDTO, IDTOIdentity, new()
      where TBusinessServiceManager : IBusinessService<TEntity>
    {
        protected TEntity Entity { get; set; } = new TEntity();
        protected TDTO DTO { get; set; } = new TDTO();
        protected readonly IServiceScopeFactory ScopeFactory;
        protected readonly TBusinessServiceManager BusinessServiceManager;
        protected readonly IMapper MapperManager;
        protected readonly IDateTimeService DateTimeService;
        protected readonly ILogger<TEntity> HealthLogger;

        protected readonly IConfiguration ConfigSettings;


        protected int CurrentPageNumber
        {
            get
            {
                if (_currentPageNumber < 1)
                {
                    _currentPageNumber = 1;
                }
                return _currentPageNumber;
            }
            set
            {
                _currentPageNumber = value;
            }
        }
        private int _currentPageNumber { get; set; }


        public EcommerceControllerBase(ILogger<TEntity> logger
                                  , TBusinessServiceManager businessService
                                  , IDateTimeService dateTimeBusinessServices
                                  , IMapper mapper
                                  , IConfiguration configuration
                                  , IServiceScopeFactory scopeFactory)
        {
            BusinessServiceManager = businessService;
            ScopeFactory = scopeFactory;
            this.DateTimeService = dateTimeBusinessServices;
            HealthLogger = logger;
            MapperManager = mapper;

            ConfigSettings = configuration;
        }

        [HttpPost]
        protected virtual async Task<IActionResult> AddAsync([FromBody] TDTO item)
        {
            var name = typeof(TEntity).Name;

            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest($"Invalid {name} State");
                }

                TEntity result = MapDTOToEntity(item);
                SetAuditInformation(result);
                await BusinessServiceManager.AddAsync(result);

                return CreatedAtAction($"GetAsync", result.Id);

            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(AddAsync)} of {name}Controller");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        protected virtual async Task<IActionResult> GetAsync(int id)
        {
            var name = typeof(TEntity).Name;

            try
            {
                var entity = await BusinessServiceManager.GetAsync(id);
                TDTO result = ConvertEntityToDTO(entity);

                if (result == null)
                {
                    return NotFound(id);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(GetAsync)} of {name}Controller with Id : {id}");
                return StatusCode(500);
            }
        }


        [HttpGet]
        protected virtual async Task<IActionResult> ListAsync([FromQuery] int? pageNumber)
        {
            var name = typeof(TEntity).Name;

            try
            {
                CurrentPageNumber = pageNumber ?? 0;

                var entities = await BusinessServiceManager.ListAsync(CurrentPageNumber) as List<TEntity>;
                CurrentPageNumber = 0;

                if (entities == null || !entities.Any())
                {
                    return NotFound();
                }

                var result = MapperManager.Map<List<TDTO>>(entities);
                return Ok(result);

            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(ListAsync)} of {name}Controller with pageNumber : {pageNumber}");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        protected virtual async Task<IActionResult> UpdateAsync([FromBody] TDTO item, int id)
        {
            var name = typeof(TEntity).Name;

            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest($"Invalid {name} State");
                }

                item.Id = id;
                var entity = await TrackedEntityForUpdateAsync(item);

                if (entity == null)
                {
                    return NotFound(item.Id);
                }

                SetAuditInformation(entity, true);
                await BusinessServiceManager.UpdateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at {nameof(UpdateAsync)} of {name}Controller");
                return StatusCode(500);
            }
        }


        protected async Task<TEntity> TrackedEntityForUpdateAsync(TDTO TDTO)
        {
            TEntity entity = await BusinessServiceManager.GetAsync(TDTO.Id);

            var _ = entity ?? throw new ArgumentNullException($"{typeof(TEntity).Name} is not found for id : {TDTO.Id}");

            return SetUpdateAuditInformation(TDTO, entity);
        }

        protected virtual TEntity SetUpdateAuditInformation<T>(T TDTO, TEntity entity) where T : IDTO
        {
            // Store the created by audit information
            var createdBy = entity.CreatedBy;
            var createdOn = entity.CreatedDate;
            MapperManager.Map(TDTO, entity);

            // Set the created by audit information as it may be lost during mapping
            entity.CreatedBy = createdBy;
            entity.CreatedDate = createdOn;

            return entity;
        }

        protected virtual TEntity SetUpdateAuditInformation(TDTO TDTO, TEntity entity)
        {
            // Store the created by audit information
            var createdBy = entity.CreatedBy;
            var createdOn = entity.CreatedDate;
            MapperManager.Map(TDTO, entity);

            // Set the created by audit information as it may be lost during mapping
            entity.CreatedBy = createdBy;
            entity.CreatedDate = createdOn;

            return entity;
        }

        protected virtual void SetAuditInformation(TEntity entity, bool isUpdate = false, Guid? currentUserId = null)
        {
            var utcTime = DateTimeService.CurrentDateTime;
            currentUserId ??= Guid.NewGuid();
            var date = new DateTime(utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute, utcTime.Second, DateTimeKind.Utc);

            if (entity != null)
            {
                if (!isUpdate)
                {
                    entity.CreatedBy = currentUserId.Value;
                    entity.CreatedDate = date;
                }
                else
                {
                    entity.UpdatedBy = currentUserId;
                    entity.UpdatedDate = date;
                }
            }
        }

        protected virtual TDTO ConvertEntityToDTO(TEntity entity)
        {
            return MapperManager.Map<TEntity, TDTO>(entity);
        }

        protected virtual TEntity MapDTOToEntity(TDTO item)
        {
            return MapperManager.Map<TDTO, TEntity>(item);
        }

        protected virtual TOtherEntity2 MapDTOToEntityWithNoID<TOtherviewModel2, TOtherEntity2>(TOtherviewModel2 item)
         where TOtherEntity2 : IEntityIdentity
        where TOtherviewModel2 : IDTONoID, new()
        {
            return MapperManager.Map<TOtherviewModel2, TOtherEntity2>(item);
        }
    }
}
