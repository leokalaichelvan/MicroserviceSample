using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MicroserviceSample
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("hosting.json", optional: true)
               .Build();

            var host = new WebHostBuilder()
              .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
        private static string[] GetServerUrls(string[] args)
        {
            List<string> urls = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                if ("--server.urls".Equals(args[i], StringComparison.OrdinalIgnoreCase))
                {
                    urls.Add(args[i + 1]);
                }
            }
            return urls.ToArray();
        }
    }
}
