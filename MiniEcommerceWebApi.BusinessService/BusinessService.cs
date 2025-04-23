using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MiniEcommerceWebApi.BusinessService.Interface;
using MiniEcommerceWebApi.BusinessService.Interface.Utilities;
using MiniEcommerceWebApi.Core.Interface;
using MiniEcommerceWebApi.Repository.Interface;

using System.Runtime.CompilerServices;

namespace MiniEcommerceWebApi.BusinessService
{
    public abstract class BusinessServiceBase<TEntity, TRepositoryManager> : IBusinessService<TEntity>
      where TEntity : IEntity, IEntityIdentity, new()
      where TRepositoryManager : IRepository<TEntity>
    {
        protected TEntity Entity { get; set; } = new TEntity();
        protected ILogger<TEntity> HealthLogger { get; set; }
        protected readonly TRepositoryManager RepositoryManager;
        protected readonly IDateTimeService DateTimeService;

        protected readonly IServiceScopeFactory ScopeFactory;

        public BusinessServiceBase(TRepositoryManager repository
                                 , IDateTimeService dateTimeBusinessServices
                                 , ILogger<TEntity> logger
                                 , IServiceScopeFactory scopeFactory)
        {
            HealthLogger = logger;
            RepositoryManager = repository;
            ScopeFactory = scopeFactory;

        }

        public virtual async Task<int> AddAsync(TEntity item)
        {
            CheckIfNull(item);
            CheckIfAddedEntityHasId(item.Id);
            var id = await RepositoryManager.AddAsync(item);
            return id;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            ValidateId(id);
            var result = await RepositoryManager.GetAsync(id);

            return result;
        }

        public virtual async Task<List<TEntity>> ListAsync(int pageNumber)
        {
            var result = await RepositoryManager.ListAsync(pageNumber);
            return result;
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            CheckIfNull(item);
            ValidateId(item?.Id);

            await RepositoryManager.UpdateAsync(item);
        }

        protected void CheckIfNull(TEntity item)
        {
            var typeName = Entity?.GetType()?.Name;
            if (item == null)
            {
                throw new NullReferenceException($"Invalid {typeName} item");
            }

        }

        protected void CheckIfAddedEntityHasId(int id, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string caller = "", [CallerMemberName] string memberName = "")
        {
            if (id != 0)
            {
                throw new Exception($"Invalid {Entity?.GetType()?.Name} parameter, method name:{memberName}, class name: {caller}, line number: {lineNumber}");
            }
        }
        protected void ValidateId(int? id, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string caller = "", [CallerMemberName] string memberName = "")
        {
            if (id == default || id == null || id == 0)
            {
                throw new Exception($"Invalid {Entity?.GetType()?.Name} parameter, method name:{memberName}, class name: {caller}, line number: {lineNumber}");
            }
        }
    }
}
