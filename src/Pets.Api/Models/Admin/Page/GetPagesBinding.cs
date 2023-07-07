namespace Pets.Api.Models.Admin.Page;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

public sealed class GetPagesBinding
{
    /// <summary>
    ///     Организация которой принадлежат страницы
    /// </summary>
    [Required]
    [FromRoute]
    public Guid OrganisationId { get; set; }
    
    public Int32 Limit { get; set; } = 8;

    public Int32 Offset { get; set; } = 0;

    public sealed class GetPagesBindingValidator : AbstractValidator<GetPagesBinding>
    {
        public GetPagesBindingValidator()
        {
            RuleFor(_ => _.OrganisationId)
                .NotEmpty()
                .NotNull();
            RuleFor(_ => _.Limit)
                .InclusiveBetween(1, 100);
            RuleFor(_ => _.Offset)
                .GreaterThanOrEqualTo(0);
        }
    }
}