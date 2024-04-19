using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Test
{
    public class CustomWebApplicationFactory<TestStartup> : WebApplicationFactory<TestStartup>where TestStartup:class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Configure the web host
            builder.UseEnvironment("Testing");
            builder.UseStartup<TestStartup>();
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            // Create and configure the host builder
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TestStartup>();
                });
        }
    }
}
