using System;
using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using WebApi;

namespace GraphQlApi.Tests.Fixture
{
    public class WebApiFixture : IDisposable
    {
        public HttpClient Client { get; private set; }

        public WebApiFixture()
        {
            Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", "on");
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<Startup>();
                });
            Client = hostBuilder.Start().GetTestServer().CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
