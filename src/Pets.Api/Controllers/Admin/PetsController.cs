using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Authorization;
using Pets.Api.Models.Admin.Pets;
using Pets.Domain.Pet;
using Pets.Queries.Pets;
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
        public async Task<IActionResult> Create(
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
                    animalId: binding.AnimalId,
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

        /// <summary>
        /// Update pet
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="petId"></param>
        /// <returns></returns>
        [HttpPatch("{petId:guid}")]
        [ProducesResponseType(typeof(PetView), 200)]
        public async Task<IActionResult> Patch(
            [FromRoute] Guid petId,
            [FromServices] IQueryProcessor processor,
            [FromServices] IPetUpdateService petUpdateService,
            [FromBody] UpdatePetBinding binding,
            CancellationToken cancellationToken)
        {
            try
            {
                await petUpdateService.Update(
                    petId: petId,
                    organisationId: HttpContext.GetOrganisationId(),
                    afterPhotoLink: binding.AfterPhotoLink,
                    beforePhotoLink: binding.BeforePhotoLink,
                    mdShortBody: binding.MdShortBody,
                    mdBody: binding.MdBody,
                    cancellationToken: cancellationToken);
            }
            catch (NotFoundException e)
            {
                return Conflict(new ProblemDetails
                {
                    Status = (Int32) HttpStatusCode.NotFound,
                    Type = "pet_not_found",
                    Detail = e.Message,
                });
            }

            return Ok(await processor.Process<GetPetQuery, PetView?>(new GetPetQuery(
                organisationId: User.GetOrganisationId(),
                petId: petId
            ), cancellationToken));
        }
        
        /// <summary>
        /// Update pet name
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="petId"></param>
        /// <returns></returns>
        [HttpPatch("{petId:guid}/name")]
        [ProducesResponseType(typeof(PetView), 200)]
        public async Task<IActionResult> PatchName(
            [FromRoute] Guid petId,
            [FromServices] IQueryProcessor processor,
            [FromServices] IPetUpdateService petUpdateService,
            [FromBody] PatchNamePetBinding binding,
            CancellationToken cancellationToken)
        {
            try
            {
                await petUpdateService.UpdateName(
                    petId: petId,
                    organisationId: HttpContext.GetOrganisationId(),
                    name: binding.Name,
                    reason: binding.Reason,
                    cancellationToken: cancellationToken);
            }
            catch (NotFoundException e)
            {
                return Conflict(new ProblemDetails
                {
                    Status = (Int32) HttpStatusCode.NotFound,
                    Type = "pet_not_found",
                    Detail = e.Message,
                });
            }

            return Ok(await processor.Process<GetPetQuery, PetView?>(new GetPetQuery(
                organisationId: User.GetOrganisationId(),
                petId: petId
            ), cancellationToken));
        }
        
        /// <summary>
        /// Update pet gender
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="petId"></param>
        /// <returns></returns>
        [HttpPatch("{petId:guid}/gender")]
        [ProducesResponseType(typeof(PetView), 200)]
        public async Task<IActionResult> UpdateGender(
            [FromRoute] Guid petId,
            [FromServices] IQueryProcessor processor,
            [FromServices] IPetUpdateService petUpdateService,
            [FromBody] UpdateGenderPetBinding binding,
            CancellationToken cancellationToken)
        {
            try
            {
                await petUpdateService.SetGender(
                    petId: petId,
                    organisationId: HttpContext.GetOrganisationId(),
                    gender: binding.Gender,
                    cancellationToken: cancellationToken);
            }
            catch (NotFoundException e)
            {
                return Conflict(new ProblemDetails
                {
                    Status = (Int32) HttpStatusCode.NotFound,
                    Type = "pet_not_found",
                    Detail = e.Message,
                });
            }

            return Ok(await processor.Process<GetPetQuery, PetView?>(new GetPetQuery(
                organisationId: User.GetOrganisationId(),
                petId: petId
            ), cancellationToken));
        }
        
        /// <summary>
        /// Update pet state
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="petId"></param>
        /// <returns></returns>
        [HttpPatch("{petId:guid}/state")]
        [ProducesResponseType(typeof(PetView), 200)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] Guid petId,
            [FromServices] IQueryProcessor processor,
            [FromServices] IPetUpdateService petUpdateService,
            [FromBody] UpdateStatusPetBinding binding,
            CancellationToken cancellationToken)
        {
            try
            {
                await petUpdateService.SetStatus(
                    petId: petId,
                    organisationId: HttpContext.GetOrganisationId(),
                    state: binding.State,
                    cancellationToken: cancellationToken);
            }
            catch (NotFoundException e)
            {
                return Conflict(new ProblemDetails
                {
                    Status = (Int32) HttpStatusCode.NotFound,
                    Type = "pet_not_found",
                    Detail = e.Message,
                });
            }

            return Ok(await processor.Process<GetPetQuery, PetView?>(new GetPetQuery(
                organisationId: User.GetOrganisationId(),
                petId: petId
            ), cancellationToken));
        }
    }
}