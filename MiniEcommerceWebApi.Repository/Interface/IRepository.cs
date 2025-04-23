using MiniEcommerceWebApi.Core.Interface;

using System.Linq.Expressions;

namespace MiniEcommerceWebApi.Repository.Interface
{
    public interface IRepository<T> where T : IEntity
    {
        Task<int> AddAsync(T item);

        Task<T> GetAsync(int id);

        Task<List<T>> ListAsync(int pageNumber = 1);

        Task UpdateAsync(T item);
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, int pageNumber);

    }
}
