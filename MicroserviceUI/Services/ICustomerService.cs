using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceUI.Models;

namespace MicroserviceUI.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomer();
    }
}
