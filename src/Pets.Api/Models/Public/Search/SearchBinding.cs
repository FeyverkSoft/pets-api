namespace Pets.Api.Models.Public.Search;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
///     Модель данных для поиска
/// </summary>
public sealed class SearchBinding
{
    /// <summary>
    ///     Организация в которой выполняется поиск
    /// </summary>
    [Required]
    [FromRoute]
    public Guid OrganisationId { get; set; }

    /// <summary>
    ///     Поисковый запрос
    /// </summary>
    [Required]
    [FromQuery]
    public String Query { get; set; }

    public Int32 Limit { get; set; } = 8;

    public Int32 Offset { get; set; } = 0;

    public sealed class SearchBindingValidator : AbstractValidator<SearchBinding>
    {
        public SearchBindingValidator()
        {
            RuleFor(_ => _.OrganisationId)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Query)
                .NotEmpty()
                .NotNull()
                .Length(2, 128);

            RuleFor(_ => _.Limit)
                .InclusiveBetween(1, 20);
            RuleFor(_ => _.Offset)
                .GreaterThanOrEqualTo(0);
        }
    }
}