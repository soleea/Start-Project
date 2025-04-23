using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MiniEcommerceWebApi.Core.Interface;
using MiniEcommerceWebApi.Repository.Interface;

using System.Data;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace MiniEcommerceWebApi.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, IEntityIdentity, new()
    {

        protected TEntity Entity { get; set; } = new TEntity();
        protected int SkippedDbRecordSize { get => MaxPageSize * (CurrentPageNumber - 1); }

        protected short MaxPageSize
        {
            get
            {
                _ = short.TryParse(ConfigSetting["MaxPageSize"], out short maxPageSize);
                return maxPageSize;
            }
        }

        protected readonly IConfiguration ConfigSetting;
        protected readonly IServiceScopeFactory ScopeFactory;
        protected readonly ILogger<TEntity> HealthLogger;

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


        protected RepositoryBase(IConfiguration configuration, ILogger<TEntity> logger
            , IServiceScopeFactory scopeFactory
            )
        {
            ConfigSetting = configuration;
            HealthLogger = logger;
            ScopeFactory = scopeFactory;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            try
            {
                using (var scope = ScopeFactory.CreateScope())
                {
                    var databaseContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
                    await databaseContext.Set<TEntity>().AddAsync(entity);
                    await databaseContext.SaveChangesAsync();
                }

                var typeName = Entity?.GetType()?.Name;
                HealthLogger.LogInformation($" Successfully Added {typeName}'s Id: '{entity?.Id} ");

                var _ = entity ?? throw new NullReferenceException();
                return entity.Id;
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at  {nameof(AddAsync)}");
                throw;
            }
        }


        public virtual async Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate
            , int currentPageNumber)
        {
            try
            {
                CurrentPageNumber = currentPageNumber;

                using (var scope = ScopeFactory.CreateScope())
                {
                    var databaseContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
                    List<TEntity> results = await databaseContext.Set<TEntity>()
                                                           .Where(predicate)
                                                           .OrderBy(r => r.Id)
                                                           .Skip(SkippedDbRecordSize)
                                                           .Take(MaxPageSize)
                                                           .ToListAsync();


                    var typeName = Entity?.GetType()?.Name;
                    HealthLogger.LogInformation($" Successfully completed Find By {predicate} for {typeName} with page number {currentPageNumber} ");

                    return results;
                }
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, " error at FindByAsync ", predicate, currentPageNumber);

                throw;
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            try
            {
                using (var scope = ScopeFactory.CreateScope())
                {
                    var databaseContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

                    var entity = await databaseContext.Set<TEntity>()
                                              .Where(x => x.Id.Equals(id))
                                              .SingleOrDefaultAsync();

                    var typeName = Entity?.GetType()?.Name;


                    HealthLogger.LogInformation($" Successfully retrieved {typeName} with the Id: '{entity?.Id} ");

                    return entity;
                }
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at  {nameof(GetAsync)} with id  {id}");
                throw;
            }
        }

        public virtual async Task<List<TEntity>> ListAsync(int pageNumber)
        {
            try
            {
                CurrentPageNumber = pageNumber;

                using (var scope = ScopeFactory.CreateScope())
                {
                    var databaseContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
                    List<TEntity> result = await databaseContext.Set<TEntity>().AsNoTracking()
                                                .OrderBy(r => r.Id)
                                                .Skip(SkippedDbRecordSize)
                                                .Take(MaxPageSize)
                                                .ToListAsync();

                    var typeName = Entity?.GetType()?.Name;
                    HealthLogger.LogInformation($" Successfully retrieved {typeName}'s List with page number {pageNumber} ");
                    CurrentPageNumber = 0;

                    return result;
                }
            }
            catch (SqlNullValueException s)
            {
                HealthLogger.LogError(s, $"{nameof(SqlNullValueException)} at  {nameof(ListAsync)} with page number  {pageNumber}");

                throw;
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at  {nameof(ListAsync)} with page number  {pageNumber}");
                throw;
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            try
            {
                using (var scope = ScopeFactory.CreateScope())
                {
                    var databaseContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
                    databaseContext.Entry(entity).State = EntityState.Modified;
                    await databaseContext.SaveChangesAsync();
                }

                var typeName = Entity?.GetType()?.Name;
                HealthLogger.LogInformation($" Successfully Updated {typeName}'s Id: '{entity?.Id} ");
            }
            catch (Exception ex)
            {
                HealthLogger.LogError(ex, $"error at  {nameof(UpdateAsync)} ");
                throw;
            }
        }
    }
}
