using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Queries.Organisation;

using Query.Core;

namespace Pets.Api.Controllers.Public
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public sealed class OrganisationController : ControllerBase
    {
        /// <summary>
        /// Get contact list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:guid}/contacts")]
        [ProducesResponseType(typeof(IEnumerable<ContactView>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetContacts(
            [FromRoute] Guid organisationId,
            [FromServices] IQueryProcessor processor,
            CancellationToken cancellationToken)
        {
            var result = await processor.Process<GetContactsQuery, IEnumerable<ContactView>>(new GetContactsQuery(
                organisationId: organisationId
            ), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get res list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:guid}/building")]
        [ProducesResponseType(typeof(IEnumerable<ResourceView>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetBuilding(
            [FromRoute] Guid organisationId,
            [FromServices] IQueryProcessor processor,
            CancellationToken cancellationToken)
        {
            var result = await processor.Process<GetBuildingQuery, IEnumerable<ResourceView>>(new GetBuildingQuery(
                organisationId: organisationId
            ), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get res list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId:guid}/needs")]
        [ProducesResponseType(typeof(IEnumerable<NeedView>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetNeeds(
            [FromRoute] Guid organisationId,
            [FromServices] IQueryProcessor processor,
            CancellationToken cancellationToken)
        {
            var result = await processor.Process<GetNeedQuery, IEnumerable<NeedView>>(new GetNeedQuery(
                organisationId: organisationId
            ), cancellationToken);
            return Ok(result);
        }
    }
}