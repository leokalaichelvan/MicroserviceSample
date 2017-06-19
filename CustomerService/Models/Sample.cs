using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerService.Models
{
    public class Sample
    {

        public async static Task InitializeGTOLabDatabase(IServiceProvider serviceProvider)
        {
            if (ShouldDropCreateDatabase())
            {

                using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var db = serviceScope.ServiceProvider.GetService<CustomerContext>();

                    if (db.Database.EnsureCreated())
                    {
                        await InsertTestData(serviceProvider);

                    }
                }
            }

        }

        private async static Task InsertTestData(IServiceProvider serviceProvider)
        {
            await AddOrUpdateAsync(serviceProvider, a => a.Name, Customer.Select(cust => cust.Value));
        }


        private static Dictionary<string, Customer> customer;
        public static Dictionary<string, Customer> Customer
        {
            get
            {
                if (customer == null)
                {
                    var custList = new Customer[]
                    {
                        new Customer { ID=1, Name = "Raju"},
                        new Customer { ID=2, Name = "Bala"},
                        new Customer { ID=3, Name = "Sherin" },
                        new Customer { ID=4, Name = "Jain"},
                        new Customer { ID=5, Name = "Naren" },
                        new Customer { ID=6, Name = "Anurine" }
                    };

                    customer = new Dictionary<string, Customer>();
                    foreach (Customer cust in custList)
                    {
                        customer.Add(cust.Name, cust);
                    }
                }

                return customer;
            }
        }

        private async static Task AddOrUpdateAsync<TEntity>(
          IServiceProvider serviceProvider,
          Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
          where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<CustomerContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<CustomerContext>();
                foreach (var item in entities)
                {
                    //var exists = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)));
                    //if (!exists)
                    //    db.Entry(item).State = EntityState.Added;

                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                       ? EntityState.Modified
                       : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }

        private static bool ShouldDropCreateDatabase()
        {
            string index = Environment.GetEnvironmentVariable("CF_INSTANCE_INDEX");
            if (string.IsNullOrEmpty(index))
            {
                return true;
            }
            int indx = -1;
            if (int.TryParse(index, out indx))
            {
                if (indx > 0) return false;
            }
            return true;
        }
    }
}
