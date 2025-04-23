using MiniEcommerceWebApi.Core.Interface;

namespace MiniEcommerceWebApi.BusinessService.Interface
{
    public interface IBusinessService<T> where T : IEntity
    {
        Task<int> AddAsync(T item);

        Task<T> GetAsync(int id);

        Task<List<T>> ListAsync(int pageNumber = 1);

        Task UpdateAsync(T item);

    }
}
