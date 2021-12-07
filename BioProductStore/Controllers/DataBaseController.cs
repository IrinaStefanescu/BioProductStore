using BioProductStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BioProductStore.Data.BioProductStoreContext;

namespace BioProductStore.Controller
{
    public class DataBaseController : ControllerBase
    {
        private readonly BioProductStoreContext _context;

        public DataBaseController(BioProductStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.DataBaseModels.ToListAsync());
        }
    }
}
