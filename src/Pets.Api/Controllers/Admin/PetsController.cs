using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pets.Api.Authorization;
using Pets.Api.Models.Admin.Pets;
using Pets.Queries;
using Pets.Queries.Pets;
using Pets.Types;

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
            [FromServices] IQueryProcessor _processor,
            [FromBody] CreatePetBinding binding,
            CancellationToken cancellationToken)
        {
            // тут создание пета


            var result = await _processor.Process<GetPetsQuery, Page<PetView>>(new GetPetsQuery(
                organisationId: User.GetOrganisationId(),
                offset: 0,
                limit: 1,
                petId: binding.PetId,
                petStatuses: new List<PetState>
                {
                    PetState.Adopted,
                    PetState.Alive,
                    PetState.Critical,
                    PetState.Death,
                    PetState.Wanted,
                    PetState.OurPets
                }
            ), cancellationToken);

            if (!result.Items.Any())
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Type = "pet_not_found"
                });

            return Ok(result.Items.First());
        }
    }
}