using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.DTOs
{
    public class RegisterOrderDTO
    {
        public float TotalPrice { get; set; }

        public Guid UserId { get; set; } //FK
    }
}
