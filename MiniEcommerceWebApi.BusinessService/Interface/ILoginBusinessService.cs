using MiniEcommerceWebApi.Core.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.BusinessService.Interface
{
    public interface ILoginBusinessService : IBusinessService<Customer>
    {
        Task<bool> LoginAsync(Customer customer);
    }
}
