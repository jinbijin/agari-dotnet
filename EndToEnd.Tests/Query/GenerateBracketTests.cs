using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Snapshooter;
using Snapshooter.Xunit;
using WebApi;
using Xunit;

namespace EndToEnd.Tests.Query
{
    public class GenerateBracketTests
    {
        private readonly HttpClient _client;
        private readonly string _body = JsonConvert.SerializeObject(new
        {
            query = "query GenerateBracket($roundCount: Int!, $participantCount: Int!) { generateBracket(roundCount: $roundCount, participantCount: $participantCount) { rounds { games { participantNrs } } } }",
            variables = new
            {
                roundCount = 4,
                participantCount = 20
            }
        });

        public GenerateBracketTests()
        {
            Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", "on");
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<Startup>();
                });
            _client = hostBuilder.Start().GetTestServer().CreateClient();
        }

        [Fact]
        public async Task GenerateBracketSuccessTest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent(_body, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            Stream stream = await response.Content.ReadAsStreamAsync();
            string content = await new StreamReader(stream, Encoding.UTF8).ReadToEndAsync();
            object contentObject = JsonConvert.DeserializeObject(content);

            Snapshot.Match(response, SnapshotNameExtension.Create("header"));
            Snapshot.Match(contentObject, SnapshotNameExtension.Create("content"));
        }
    }
}
