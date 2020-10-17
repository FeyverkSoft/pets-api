using System;
using System.ComponentModel.DataAnnotations;

using FluentValidation;

namespace Pets.Api.Models.Pets
{
    public sealed class GetPetsBinding
    {
        /// <summary>
        /// Организация которой принадлежат петомцы
        /// </summary>
        [Required]
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// некий фильтр, пока что не понятно какие параметры
        /// </summary>
        public String? Filter { get; set; }

        public Int32 Limit { get; set; } = 8;

        public Int32 Offset { get; set; } = 0;

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
            }
        }
    }
}