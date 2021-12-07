using BioProductStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public Category Category { get; set; }
        public Guid CategpryId { get; set; }
    }
}
