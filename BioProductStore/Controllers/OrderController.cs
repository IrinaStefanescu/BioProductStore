using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Controllers
{
    [Route("api/[controller]")] //[controller] -> inseamna numele controllerului
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly BioProductStoreContext _context;

        public OrderController(IOrderService orderService, BioProductStoreContext context)
        {
            _orderService = orderService;
            _context = context;
        }

        //GET
        [HttpGet("byId")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(_orderService.GetOrderByOrderId(Id));
        }


        [HttpGet("allOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }


        //POST
        [HttpPost("create")]
        public IActionResult Create(RegisterOrderDTO order)
        {
            _orderService.CreateOrder(order);
            return Ok();
        }

        //PUT
        [HttpPut("update")]
        public IActionResult Update(UpdateOrderDTO order, Guid id)
        {
            _orderService.UpdateOrder(order, id);
            return Ok();
        }


        //DELETE
        [HttpDelete]
        public IActionResult DeleteById(Guid Id)
        {
            _orderService.DeleteOrderById(Id);
            return Ok();
        }
    }
}
