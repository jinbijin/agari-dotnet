using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GraphQlApi.Tests.Instrumentation
{
    public static class HttpResponseHelper
    {
        public static async Task<object> ContentAsObject(this HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            string content = await new StreamReader(stream, Encoding.UTF8).ReadToEndAsync();
            return JsonConvert.DeserializeObject(content);
        }
    }
}
