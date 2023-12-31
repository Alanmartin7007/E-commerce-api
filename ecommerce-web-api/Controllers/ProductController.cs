﻿using ecommerce_web_api.Models;
using ecommerce_web_api.Services.Categories;
using ecommerce_web_api.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var data = _productService.GetProducts();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductbyId(int id)
        {
            var data = _productService.GetProductById(id);
            if (data == null)
            {
                return NotFound("No product Found with Id:" + id);
            }
            return Ok(data);
        }

        [HttpGet("category/{catId}")]
      public ActionResult<List<Product>> GetProductsByCatId(int catId)
        {
            return _productService.GetProductsByCatId(catId);
        }

        

        [HttpPost("{id}")]
        public ActionResult<Product> Postproduct([FromBody] Product product,int id)
        {
            var data = _productService.Addproduct(product,id);
            if (data == null)
            {
                return BadRequest();
            }
            return Created("", data);

        }
        [HttpPut("{id}/{catid}")]
        public ActionResult<Product> PutProduct(int id, [FromBody] Product product,int catid)
        {
            var data = _productService.GetProductById(id);
            if (data == null)
            { return NotFound("No product found with id :" + id); }
            var response = _productService.UpdateProduct(id, product,catid);
            return Ok(response);



        }

        [HttpDelete("{id}")]
        public ActionResult<string> Deleteproduct(int id)
        {
            var data = _productService.GetProductById(id);
            if (data == null)
            {
                return NotFound("Not product found with id:" + id);
            }
            _productService.DeleteProduct(id);
            return Ok("product deleted");
        }
    }
}
