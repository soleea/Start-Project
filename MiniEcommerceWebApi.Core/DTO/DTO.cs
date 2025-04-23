using MiniEcommerceWebApi.Core.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Core.DTO
{
    public class DTO : DTOIdentity, IDTO
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }


        public Guid CreatedBy { get; set; }
    }
}
