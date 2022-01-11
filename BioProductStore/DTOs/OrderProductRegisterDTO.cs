using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.DTOs
{
    public class OrderProductRegisterDTO
    {
        public Guid OrderId { get; set; }  //FK

        public Guid ProductId { get; set; }   //FK
    }
}
