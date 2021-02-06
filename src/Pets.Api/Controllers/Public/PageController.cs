using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Models.Public.Page;
using Pets.Queries.Pages;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public sealed class PageController : ControllerBase
    {
        /// <summary>
        /// Get page
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor _processor,
            [FromQuery] GetPageBinding binding,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetPageQuery, PageView?>(new GetPageQuery(
                organisationId: binding.OrganisationId,
                page: binding.Page
            ), cancellationToken);
            if (result == null)
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Type = "page_not_found"
                });
            return Ok(result);
        }
    }
}