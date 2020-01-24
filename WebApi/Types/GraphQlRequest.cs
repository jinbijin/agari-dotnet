using Newtonsoft.Json.Linq;

namespace WebApi.Types
{
    public class GraphQlRequest
    {
        public string? OperationName { get; set; }
        public string Query { get; set; } = string.Empty;
        public JObject Variables { get; set; } = new JObject();
    }
}
