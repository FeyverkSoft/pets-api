using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Models.Public.News;
using Pets.Queries;
using Pets.Queries.News;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
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
                tag: binding.Tag,
                petId: binding.PetId
            ), cancellationToken));
        }

        /// <summary>
        /// Get news
        /// </summary>
        /// <param name="organisationId">идентификатор орагнизации</param>
        /// <param name="newsId">идентификатор конкретной новости</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/{newsId}")]
        [ProducesResponseType(typeof(NewsView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor _processor,
            [FromRoute] Guid organisationId,
            [FromRoute] Guid newsId,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetNewsQuery, Page<NewsView>>(new GetNewsQuery(
                organisationId: organisationId,
                offset: 0,
                limit: 1,
                newsId: newsId
            ), cancellationToken);

            if (!result.Items.Any())
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Type = "news_not_found"
                });

            return Ok(result.Items.First());
        }
    }
}