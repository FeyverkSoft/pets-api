using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Models.Public.Search;
using Pets.Queries;
using Pets.Queries.Search;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public sealed class SearchController : ControllerBase
    {
        /// <summary>
        /// Search documents and pets
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{OrganisationId}")]
        [ProducesResponseType(typeof(Page<SearchView>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor processor,
            [FromQuery] SearchBinding binding,
            CancellationToken cancellationToken)
        {
            return Ok(await processor.Process<SearchQuery,Page<SearchView>>(new SearchQuery(
                organisationId: binding.OrganisationId,
                query: binding.Query,
                limit: binding.Limit,
                offset: binding.Offset
                ), cancellationToken));
        }
    }
}