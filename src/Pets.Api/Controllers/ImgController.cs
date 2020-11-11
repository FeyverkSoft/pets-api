using Microsoft.AspNetCore.Mvc;

using Pets.Queries.Documents;

using Query.Core;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pets.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public sealed class ImgController : ControllerBase
    {
        /// <summary>
        /// Get contact list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Stream), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetImage(
            [FromRoute] Guid id,
            [FromServices] IQueryProcessor _processor,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetImgQuery, DocumentInfo?>(new GetImgQuery(
                imageId: id
            ), cancellationToken);
            if (result == null)
                return NotFound();
            result.Stream.Position = 0;
            Response.ContentType = result.ContentType;
            return Ok(result.Stream);
        }
    }
}
