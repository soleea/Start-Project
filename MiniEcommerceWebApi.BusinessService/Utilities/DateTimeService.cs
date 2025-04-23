using MiniEcommerceWebApi.BusinessService.Interface.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.BusinessService.Utilities
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime CurrentDateTime { get => DateTime.UtcNow; }
    }
}
