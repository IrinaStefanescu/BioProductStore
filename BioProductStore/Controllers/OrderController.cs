﻿using System;
using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

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
        //[AuthorizationAttribute(Role.Admin)]
        [HttpGet("byId/{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_orderService.GetOrderByOrderId(id));
        }


        [HttpGet("allOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet("allOrdersForAUser/{id}")]
        public IActionResult GetAllOrdersForAUser(Guid id)
        {
            return Ok(_orderService.GetAllOrdersForAUser(id));
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
