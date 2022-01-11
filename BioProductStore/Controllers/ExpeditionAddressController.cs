using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Services.ExpeditionAddressService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Controllers
{
    [Route("api/[controller]")] //[controller] -> inseamna numele controllerului
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private readonly IExpeditionAddressService _expeditionAddressService;
        private readonly BioProductStoreContext _context;

        public DeliveryAddressController(IExpeditionAddressService deliveryAddressService, BioProductStoreContext context)
        {
            _expeditionAddressService = deliveryAddressService;
            _context = context;
        }

        //GET
        [HttpGet("byId/{id}")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(_expeditionAddressService.GetExpeditionAddressByExpeditionAddressId(Id));
        }


        [HttpGet("allDeliveryAddresses")]
        public IActionResult GetAllDeliveryAddresses()
        {
            return Ok(_expeditionAddressService.GetAllExpeditionAddresses());
        }


        //POST
        [HttpPost("create")]
        public IActionResult Create(RegisterExpeditionAddressDTO deliveryAddress)
        {
            _expeditionAddressService.CreateExpeditionAddress(deliveryAddress);
            return Ok();
        }

        //PUT
        [HttpPut("update")]
        public IActionResult Update(UpdateExpeditionAddressDTO expedititonAddress, Guid id)
        {
            _expeditionAddressService.UpdateExpeditionAddress(expedititonAddress, id);
            return Ok();
        }


        //DELETE
        [HttpDelete]
        public IActionResult DeleteById(Guid Id)
        {
            _expeditionAddressService.DeleteExpeditionAddressById(Id);
            return Ok();
        }


    }
}
