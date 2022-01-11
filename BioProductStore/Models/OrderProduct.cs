using BioProductStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BioProductStore.Models
{
    public class OrderProduct : BaseEntity 
    {
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
