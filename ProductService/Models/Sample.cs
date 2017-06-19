using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Models
{
    public class Sample
    {

        public async static Task InitializeGTOLabDatabase(IServiceProvider serviceProvider)
        {
            if (ShouldDropCreateDatabase())
            {

                using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var db = serviceScope.ServiceProvider.GetService<ProductContext>();

                    if (db.Database.EnsureCreated())
                    {
                        await InsertTestData(serviceProvider);

                    }
                }
            }

        }

        private async static Task InsertTestData(IServiceProvider serviceProvider)
        {
            await AddOrUpdateAsync(serviceProvider, a => a.Name, Product.Select(product => product.Value));
        }


        private static Dictionary<string, Product> product;
        public static Dictionary<string, Product> Product
        {
            get
            {
                if (product == null)
                {
                    var productList = new Product[]
                    {
                        new Product { ID=1, Name = "Television", Description="Television" , Price=25000},
                        new Product { ID=2, Name = "Washing Machine", Description="Washing Machine", Price=10000  },
                        new Product { ID=3, Name = "Fridge", Description="Fridge", Price=15000  },
                        new Product { ID=4, Name = "AC", Description="AC", Price=50000 },
                        new Product { ID=5, Name = "Dinning Table", Description="Dinning Table", Price=18000  },
                        new Product { ID=6, Name = "Chair", Description="Chair", Price=450  }
                    };

                    product = new Dictionary<string, Product>();
                    foreach (Product prod in productList)
                    {
                        product.Add(prod.Name, prod);
                    }
                }

                return product;
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
                var db = serviceScope.ServiceProvider.GetService<ProductContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ProductContext>();
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
