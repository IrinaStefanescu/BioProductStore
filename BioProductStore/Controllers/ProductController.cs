using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioProductStore.Models;
using BioProductStore.Utilities.Attributes;
using BioProductStore.Services.ProductService;

namespace BioProductStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorization(Role.Admin)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly BioProductStoreContext _context;

        public ProductController(IProductService productService, BioProductStoreContext context)
        {
            _productService = productService;
            _context = context;
        }

        //GET
        [HttpGet("byId/{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_productService.GetProductByProductId(id));
        }


        [HttpGet("allCategories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_productService.GetAllProducts());
        }


        //POST
        [HttpPost("create")]
        public IActionResult Create(RegisterProductDTO product)
        {
            _productService.CreateProduct(product);
            return Ok();
        }

        //PUT
        [HttpPut("update")]
        public IActionResult Update(UpdateProductDTO product, Guid id)
        {
            _productService.UpdateProduct(product, id);
            return Ok();
        }


        //DELETE
        [HttpDelete]
        public IActionResult DeleteById(Guid Id)
        {
            _productService.DeleteProductById(Id);
            return Ok();
        }
    }
}
