using BioProductStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.OrderProductRelationshipService
{
    public interface IOrderProductService
    {
        public void CreateOrderProductRelation(OrderProductRegisterDTO orderProductRelation);
    }
}
