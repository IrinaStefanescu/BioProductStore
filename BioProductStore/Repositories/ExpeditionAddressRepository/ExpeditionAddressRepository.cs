using BioProductStore.Data;
using BioProductStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.ExpeditionAddressRepository
{
    public class ExpeditionAddressRepository : GenericRepository<ExpeditionAddress>, IExpeditionAddressRepository
    {
        private readonly BioProductStoreContext _context;

        public ExpeditionAddressRepository(BioProductStoreContext context) : base(context)
        {
            _context = context;
        }

        public List<ExpeditionAddress> GetAllExpeditionAddresses()
        {
            return new List<ExpeditionAddress>(_context.ExpeditionAddresses.AsNoTracking().ToList());
        }
    }
}
