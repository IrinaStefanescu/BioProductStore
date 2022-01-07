using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.ExpeditionAddressRepository
{
    public interface IExpeditionAddressRepository : IGenericRepository<ExpeditionAddress>
    {
        List<ExpeditionAddress> GetAllExpeditionAddress();
    }
}
