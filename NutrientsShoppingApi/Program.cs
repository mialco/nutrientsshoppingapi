using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace NutrientsShoppingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
				//.UseUrls("http://localhost:3001")
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseSetting("detailedErrors","true")
                .UseIISIntegration()
                .UseStartup<Startup>()
				.CaptureStartupErrors(true)
                //.UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
