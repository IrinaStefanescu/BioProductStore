using BioProductStore.DTOs;
using BioProductStore.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using BioProductStore.Data;
using System;

namespace BioProductStore.Controllers
{
    //[controller] -> name of the controller
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly BioProductStoreContext _context;

        public UserController(IUserService userService, BioProductStoreContext context)
        {
            _userService = userService;
            _context = context;
        }

        //GET
        [HttpGet("byId")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(_userService.GetUserByUserId(Id));
        }
        //GET ALL
        [HttpGet("allUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        //GETbyNAME
        [HttpGet("byName")]
        public IActionResult GetAllUsersByName(string name)
        {
            return Ok(_userService.GetAllUsersByName(name));
        }

     


        //POST
        [HttpPost("create")]
        public IActionResult Create(RegisterUserDTO user)
        {
            _userService.CreateUser(user);
            return Ok();
        }

        //PUT
        [HttpPut("updateUser")]
        public IActionResult Update(RegisterUserDTO user, Guid id)
        {
            _userService.UpdateUser(user, id);
            return Ok();
        }


        //DELETE
        [HttpDelete]
        public IActionResult DeleteById(Guid Id)
        {
            _userService.DeleteUserById(Id);
            return Ok();
        }
    }
 }
