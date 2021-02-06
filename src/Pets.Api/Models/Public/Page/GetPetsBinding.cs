using System;
using System.ComponentModel.DataAnnotations;

using FluentValidation;

namespace Pets.Api.Models.Public.Page
{
    public sealed class GetPageBinding
    {
        /// <summary>
        /// Организация которой принадлежит страница
        /// </summary>
        [Required]
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Наименование страницы
        /// </summary>
        public String Page { get; set; }

        public sealed class GetPageBindingValidator : AbstractValidator<GetPageBinding>
        {
            public GetPageBindingValidator()
            {
                RuleFor(_ => _.OrganisationId)
                    .NotEmpty()
                    .NotNull();
                RuleFor(_ => _.Page)
                    .NotEmpty()
                    .NotNull();
            }
        }
    }
}