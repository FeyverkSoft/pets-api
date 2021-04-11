using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Authorization;
using Pets.Api.Models.Admin.Pets;
using Pets.Domain.Pet;
using Pets.Queries;
using Pets.Queries.Pets;
using Pets.Types;
using Pets.Types.Exceptions;

using Query.Core;

namespace Pets.Api.Controllers.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Policy = "admin")]
    [ApiController]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 401)]
    [Route("admin/[controller]")]
    public sealed class PetsController : ControllerBase
    {
        /// <summary>
        /// Create new pet
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PetView), 200)]
        public async Task<IActionResult> Get(
            [FromServices] IQueryProcessor processor,
            [FromServices] IPetCreateService petCreateService,
            [FromBody] CreatePetBinding binding,
            CancellationToken cancellationToken)
        {
            try
            {
                await petCreateService.Create(
                    petId: binding.PetId,
                    organisationId: HttpContext.GetOrganisationId(),
                    name: binding.Name,
                    gender: binding.PetGender,
                    type: binding.Type,
                    petState: binding.PetState,
                    afterPhotoLink: binding.AfterPhotoLink,
                    beforePhotoLink: binding.BeforePhotoLink,
                    mdShortBody: binding.MdShortBody,
                    mdBody: binding.MdBody,
                    cancellationToken: cancellationToken);
            }
            catch (IdempotencyCheckException e)
            {
                return Conflict(new ProblemDetails
                {
                    Status = (Int32) HttpStatusCode.Conflict,
                    Type = "pet_already_exists",
                    Detail = e.Message,
                });
            }

            return Ok(await processor.Process<GetPetQuery, PetView?>(new GetPetQuery(
                organisationId: User.GetOrganisationId(),
                petId: binding.PetId
            ), cancellationToken));
        }
    }
}