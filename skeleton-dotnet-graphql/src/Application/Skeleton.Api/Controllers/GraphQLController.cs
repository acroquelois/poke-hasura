using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skeleton.Api.GraphQL.Query;

namespace Skeleton.Api.Controllers
{
    [Authorize]
    [ApiController, Route("graphql")]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public GraphQLController(ISchema schema, IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GraphQLQueryDto query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query?.Query;
               
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return Problem(detail: result.Errors.Select(_ => _.Message).FirstOrDefault(), statusCode: 500);
            }
            return Ok(result.Data);
        }
    }
}