using MiniEcommerceWebApi.Core.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Repository.Interface
{
    public interface ILoginRepository : IRepository<Customer>
    {
        Task<bool> Login(Customer customer);
    }
}
