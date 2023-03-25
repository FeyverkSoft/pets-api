namespace Pets.Api.Controllers.Admin;

using System.Net;

using Authorization;

using Domain.Pet;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models.Admin.Pets;

using Queries.Pets;

using Types.Exceptions;

/// <summary>
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
    ///     Create new pet
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PetView), 200)]
    public async Task<IActionResult> Create(
        [FromServices] IMediator processor,
        [FromServices] IPetCreateService petCreateService,
        [FromBody] CreatePetBinding binding,
        CancellationToken cancellationToken)
    {
        try
        {
            await petCreateService.Create(
                binding.PetId,
                HttpContext.GetOrganisationId(),
                binding.Name,
                binding.PetGender,
                binding.Type,
                binding.PetState,
                binding.AfterPhotoLink,
                binding.BeforePhotoLink,
                binding.MdShortBody,
                binding.MdBody,
                binding.AnimalId,
                cancellationToken);
        }
        catch (IdempotencyCheckException e)
        {
            return Conflict(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.Conflict,
                Type = "pet_already_exists",
                Detail = e.Message
            });
        }

        return Ok(await processor.Send(new GetPetQuery(
            User.GetOrganisationId(),
            binding.PetId
        ), cancellationToken));
    }

    /// <summary>
    ///     Update pet
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="petId"></param>
    /// <returns></returns>
    [HttpPatch("{petId:guid}")]
    [ProducesResponseType(typeof(PetView), 200)]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid petId,
        [FromServices] IMediator processor,
        [FromServices] IPetUpdateService petUpdateService,
        [FromBody] UpdatePetBinding binding,
        CancellationToken cancellationToken)
    {
        try
        {
            await petUpdateService.Update(
                petId,
                HttpContext.GetOrganisationId(),
                binding.AfterPhotoLink,
                binding.BeforePhotoLink,
                binding.MdShortBody,
                binding.MdBody,
                cancellationToken);
        }
        catch (NotFoundException e)
        {
            return Conflict(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "pet_not_found",
                Detail = e.Message
            });
        }

        return Ok(await processor.Send(new GetPetQuery(
            User.GetOrganisationId(),
            petId
        ), cancellationToken));
    }

    /// <summary>
    ///     Update pet name
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="petId"></param>
    /// <returns></returns>
    [HttpPatch("{petId:guid}/name")]
    [ProducesResponseType(typeof(PetView), 200)]
    public async Task<IActionResult> PatchName(
        [FromRoute] Guid petId,
        [FromServices] IMediator processor,
        [FromServices] IPetUpdateService petUpdateService,
        [FromBody] PatchNamePetBinding binding,
        CancellationToken cancellationToken)
    {
        try
        {
            await petUpdateService.UpdateName(
                petId,
                HttpContext.GetOrganisationId(),
                binding.Name,
                binding.Reason,
                cancellationToken);
        }
        catch (NotFoundException e)
        {
            return Conflict(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "pet_not_found",
                Detail = e.Message
            });
        }

        return Ok(await processor.Send(new GetPetQuery(
            User.GetOrganisationId(),
            petId
        ), cancellationToken));
    }

    /// <summary>
    ///     Update pet gender
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="petId"></param>
    /// <returns></returns>
    [HttpPatch("{petId:guid}/gender")]
    [ProducesResponseType(typeof(PetView), 200)]
    public async Task<IActionResult> UpdateGender(
        [FromRoute] Guid petId,
        [FromServices] IMediator processor,
        [FromServices] IPetUpdateService petUpdateService,
        [FromBody] UpdateGenderPetBinding binding,
        CancellationToken cancellationToken)
    {
        try
        {
            await petUpdateService.SetGender(
                petId,
                HttpContext.GetOrganisationId(),
                binding.Gender,
                cancellationToken);
        }
        catch (NotFoundException e)
        {
            return Conflict(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "pet_not_found",
                Detail = e.Message
            });
        }

        return Ok(await processor.Send(new GetPetQuery(
            User.GetOrganisationId(),
            petId
        ), cancellationToken));
    }

    /// <summary>
    ///     Update pet state
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="petId"></param>
    /// <returns></returns>
    [HttpPatch("{petId:guid}/state")]
    [ProducesResponseType(typeof(PetView), 200)]
    public async Task<IActionResult> UpdateStatus(
        [FromRoute] Guid petId,
        [FromServices] IMediator processor,
        [FromServices] IPetUpdateService petUpdateService,
        [FromBody] UpdateStatusPetBinding binding,
        CancellationToken cancellationToken)
    {
        try
        {
            await petUpdateService.SetStatus(
                petId,
                HttpContext.GetOrganisationId(),
                binding.State,
                cancellationToken);
        }
        catch (NotFoundException e)
        {
            return Conflict(new ProblemDetails
            {
                Status = (Int32)HttpStatusCode.NotFound,
                Type = "pet_not_found",
                Detail = e.Message
            });
        }

        return Ok(await processor.Send(new GetPetQuery(
            User.GetOrganisationId(),
            petId
        ), cancellationToken));
    }
}