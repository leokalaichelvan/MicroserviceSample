using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public ProductController(ProductContext dbContext)
        {
            DbContext = dbContext;
        }

        public ProductContext DbContext { get; }



        // GET: /api/customer
        [HttpGet("")]
        public async Task<List<Product>> Get()
        {
            var product = await DbContext.Product
                .ToListAsync();

            return product;
        }

        // GET api/customer/5.
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var product = DbContext.Product.Select(a => a.ID == id).ToString();

            return product;
        }

        // POST api/product
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
