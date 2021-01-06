using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Pets.Api.Models.News;
using Pets.Queries;
using Pets.Queries.News;

using Query.Core;

namespace Pets.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class NewsController : ControllerBase
    {
        /// <summary>
        /// Get news list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Page<NewsView>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor _processor,
            [FromQuery] GetNewsBinding binding,
            CancellationToken cancellationToken)
        {
            return Ok(await _processor.Process<GetNewsQuery, Page<NewsView>>(new GetNewsQuery(
                organisationId: binding.OrganisationId,
                offset: binding.Offset,
                limit: binding.Limit,
                tags: binding.Tags,
                petId: binding.PetId
            ), cancellationToken));
        }
    }
}