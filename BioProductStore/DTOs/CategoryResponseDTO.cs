using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.DTOs
{
    public class CategoryResponseDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string imgAddress { get; set; }
    }
}
