using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace IMDB.Test
{
    public class CustomWebApplicationFactory<TestStartup> : WebApplicationFactory<TestStartup>where TestStartup:class
    {
        

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
