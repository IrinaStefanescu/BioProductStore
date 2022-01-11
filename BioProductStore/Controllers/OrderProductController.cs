using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Services.OrderProductRelationshipService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")] //[controller] -> inseamna numele controllerului
    [ApiController]
    public class OrderProductRelationController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;
        private readonly BioProductStoreContext _context;

        public OrderProductRelationController(IOrderProductService orderProductRelationService, BioProductStoreContext context)
        {
            _orderProductService = orderProductRelationService;
            _context = context;
        }


        //POST
        [HttpPost("create")]
        public IActionResult Create(OrderProductRegisterDTO orderProductRelation)
        {
            _orderProductService.CreateOrderProductRelation(orderProductRelation);
            return Ok();
        }
    }
}
