using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Models
{
    public class CustomerContext: DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
    }
}
