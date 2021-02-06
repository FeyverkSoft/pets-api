using System;
using System.ComponentModel.DataAnnotations;

using FluentValidation;

namespace Pets.Api.Models.Public.News
{
    public sealed class GetNewsBinding
    {
        /// <summary>
        /// Организация к которой относятся новости
        /// </summary>
        [Required]
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Идентификатор животного по которому выбираются новости
        /// </summary>
        public Guid? PetId { get; set; }

        /// <summary>
        /// Список тегов по которым выбираются новости
        /// </summary>
        public String? Tag { get; set; }
        
        public Int32 Limit { get; set; } = 8;

        public Int32 Offset { get; set; } = 0;

        public sealed class GetNewsBindingValidator : AbstractValidator<GetNewsBinding>
        {
            public GetNewsBindingValidator()
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