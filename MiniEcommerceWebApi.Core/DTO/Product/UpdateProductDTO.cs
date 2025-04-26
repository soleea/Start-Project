using MiniEcommerceWebApi.Core.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.DTO.Product
{
    public class UpdateProductDTO : IDTONoID
    {
        public string ProductName { get; set; }
    }
}
