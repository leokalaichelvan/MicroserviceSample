using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceUI.Models;
using Pivotal.Discovery.Client;
using System.Net.Http;

namespace MicroserviceUI.Services
{
    public class CustomerService : BaseDiscoveryService, ICustomerService
    {
        public CustomerService(IDiscoveryClient client) : base(client)
        { }
        const string customerserviceURI = "http://customerservice/api/customer";

        public async Task<List<Customer>> GetCustomer()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, customerserviceURI);

            var customer = await Invoke<List<Customer>>(request);

            return customer;
        }
    }
}
