using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        public CustomerController(CustomerContext dbContext)
        {
            DbContext = dbContext;
        }

        public CustomerContext DbContext { get; }



        // GET: /api/customer
        [HttpGet("")]
        public async Task<List<Customer>> Get()
        {
            var customer = await DbContext.Customer
                .ToListAsync();

            return customer;
        }
       
        // GET api/customer/5.
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var customer = DbContext.Customer.Select(a => a.ID == id).ToString();

            return customer;
        }

        // POST api/customer
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
