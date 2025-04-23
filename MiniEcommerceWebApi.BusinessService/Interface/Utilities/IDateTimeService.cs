using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.BusinessService.Interface.Utilities
{
    public interface IDateTimeService
    {
        DateTime CurrentDateTime { get; }
    }
}
