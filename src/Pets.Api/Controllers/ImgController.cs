using Microsoft.AspNetCore.Mvc;

using Pets.Queries.Documents;

using Query.Core;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Pets.Api.Models.Img;
using Pets.Domain.Documents;

namespace Pets.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public sealed class ImgController : ControllerBase
    {
        /// <summary>
        /// Get image by id
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
            return File(result.Stream, result.ContentType);
        }

        /// <summary>
        /// save image
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <code>validation error, see <see href="https://tools.ietf.org/html/rfc7807#section-3">rfc7807#section-3</see></code>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UploadFileView), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> PutImage(
            [FromForm] UploadFileBinding file,
            [FromServices] IDocumentRepository _fileStoreService,
            CancellationToken cancellationToken)
        {
            var fileId = await _fileStoreService.SaveFileAsync(file.File, cancellationToken);
            return Ok(new UploadFileView(fileId));
        }
    }
}