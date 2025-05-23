﻿using MiniEcommerceWebApi.Core.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.DTO.Product
{
    public class CreateProductDTO: IDTONoID
    {
        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal UnitPrice { get; set; }
    }
}
