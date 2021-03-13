using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using FluentValidation;

using Pets.Types;

namespace Pets.Api.Models.Public.Pets
{
    public sealed class GetPetsBinding
    {
        /// <summary>
        /// Организация которой принадлежат петомцы
        /// </summary>
        [Required]
        public Guid OrganisationId { get; set; }

        public Int32 Limit { get; set; } = 8;

        public Int32 Offset { get; set; } = 0;

        /// <summary>
        /// Статусы животных для отображения
        /// </summary>
        public List<PetState> PetStatuses { get; set; } = new() { PetState.Adopted, PetState.Alive, PetState.Critical, PetState.Death, PetState.Wanted };

        /// <summary>
        /// Фильтр по полу
        /// </summary>
        public List<PetGender> Genders { get; set; } = new() { PetGender.Female, PetGender.Male, PetGender.Unset };

        /// <summary>
        /// Текстовый фильтр
        /// Пока что по имени и краткому описанию
        /// </summary>
        public String? Text { get; set; }
        
        public sealed class GetPetsBindingValidator : AbstractValidator<GetPetsBinding>
        {
            public GetPetsBindingValidator()
            {
                RuleFor(_ => _.OrganisationId)
                    .NotEmpty()
                    .NotNull();
                RuleFor(_ => _.Limit)
                    .InclusiveBetween(1, 20);
                RuleFor(_ => _.Offset)
                    .GreaterThanOrEqualTo(0);
                
                RuleFor(_ => _.Text)
                    .MaximumLength(128);
            }
        }
    }
}