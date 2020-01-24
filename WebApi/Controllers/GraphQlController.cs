using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Types;

namespace WebApi.Controllers
{
    public class GraphQlController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;

        public GraphQlController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [EnableCors]
        [HttpPost("graphql")]
        [HttpOptions("graphql")]
        public async Task<IActionResult> Post([FromBody] GraphQlRequest query)
        {
            try
            {
                var result = await _documentExecuter.ExecuteAsync(config =>
                {
                    config.Schema = _schema;
                    config.Query = query.Query;
                    config.Inputs = query.Variables.ToInputs();
                });

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
