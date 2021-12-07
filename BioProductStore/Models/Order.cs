using BioProductStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Models
{
    public class Order : BaseEntity
    {
        public float Price { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public ExpeditionAddress ExpeditionAddress { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
