using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pivotal.Discovery.Client;
using MicroserviceUI.Models;
using System.Net.Http;

namespace MicroserviceUI.Services
{
    public class ProductService : BaseDiscoveryService, IProductService
    {
        public ProductService(IDiscoveryClient client) : base(client)
        { }
        const string productserviceURI = "http://productservice/api/product";

        public async Task<List<Product>> GetProduct()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, productserviceURI);

            var product = await Invoke<List<Product>>(request);

            return product;
        }
    }
}
