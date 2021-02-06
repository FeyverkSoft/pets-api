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
        [HttpGet("{organisationId}/contacts")]
        [ProducesResponseType(typeof(IEnumerable<ContactView>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetContacts(
            [FromRoute] Guid organisationId,
            [FromServices] IQueryProcessor _processor,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetContactsQuery, IEnumerable<ContactView>>(new GetContactsQuery(
                organisationId: organisationId
            ), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get res list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/building")]
        [ProducesResponseType(typeof(IEnumerable<ResourceView>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetBuilding(
            [FromRoute] Guid organisationId,
            [FromServices] IQueryProcessor _processor,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetBuildingQuery, IEnumerable<ResourceView>>(new GetBuildingQuery(
                organisationId: organisationId
            ), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get res list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{organisationId}/needs")]
        [ProducesResponseType(typeof(IEnumerable<NeedView>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetNeeds(
            [FromRoute] Guid organisationId,
            [FromServices] IQueryProcessor _processor,
            CancellationToken cancellationToken)
        {
            var result = await _processor.Process<GetNeedQuery, IEnumerable<NeedView>>(new GetNeedQuery(
                organisationId: organisationId
            ), cancellationToken);
            return Ok(result);
        }
    }
}