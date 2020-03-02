using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQlApi.Tests.Fixture;
using GraphQlApi.Tests.Instrumentation;
using Newtonsoft.Json;
using Snapshooter;
using Snapshooter.Xunit;

namespace GraphQlApi.Tests
{
    public class GraphQlApiTest<TVar> : GraphQlApiTestBase
    {
        public GraphQlApiTest(WebApiFixture webApiFixture, string query) : base(webApiFixture, query)
        {
        }

        public async Task MatchResponseForRequest(TVar variables, SnapshotFullName fullName)
        {
            HttpRequestMessage request = Request(variables);

            HttpResponseMessage response = await _webApiFixture.Client.SendAsync(request);

            await MatchResponse(response, fullName);
        }

        private HttpRequestMessage Request(TVar variables)
        {
            return RequestFromBody(new { query = _query, variables });
        }
    }

    public class GraphQlApiTest : GraphQlApiTestBase
    {
        public GraphQlApiTest(WebApiFixture webApiFixture, string query) : base(webApiFixture, query)
        {
        }

        public async Task MatchResponseForRequest(SnapshotFullName fullName)
        {
            HttpRequestMessage request = Request();

            HttpResponseMessage response = await _webApiFixture.Client.SendAsync(request);

            await MatchResponse(response, fullName);
        }

        private HttpRequestMessage Request()
        {
            return RequestFromBody(new { query = _query });
        }
    }

    public class GraphQlApiTestBase
    {
        protected const string GraphQlEndpoint = "/graphql";
        protected const string MediaType = "application/json";

        protected readonly string _query;
        protected readonly WebApiFixture _webApiFixture;

        public GraphQlApiTestBase(WebApiFixture webApiFixture, string query)
        {
            _webApiFixture = webApiFixture;
            _query = query;
        }

        protected static HttpRequestMessage RequestFromBody(object body)
        {
            return new HttpRequestMessage(HttpMethod.Post, GraphQlEndpoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, MediaType)
            };
        }

        protected static async Task MatchResponse(HttpResponseMessage response, SnapshotFullName fullName)
        {
            Snapshot.Match(new
            {
                ResponseHeaders = response,
                ResponseContent = await response.ContentAsObject()
            }, fullName, opt => opt.IgnoreField("ResponseContent.data")); // Workaround until mocks can be bound
        }
    }
}
