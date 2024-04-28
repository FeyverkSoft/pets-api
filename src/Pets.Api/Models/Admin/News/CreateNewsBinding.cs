namespace Pets.Api.Models.Admin.News;

using System.Collections.Generic;

public sealed class CreateNewsBinding
{
    public Guid NewsId { get; set; }
    public String Title { get; set; }
    public String ImgLink { get; set; }
    public String MdShortBody { get; set; }
    public String MdBody { get; set; }
    public ICollection<Guid> LinkedPets { get; set; } = new List<Guid>();
    public ICollection<String> Tags { get; set; } = new List<String>();

    public sealed class CreateNewsBindingValidator : AbstractValidator<CreateNewsBinding>
    {
        public CreateNewsBindingValidator()
        {
            RuleFor(_ => _.NewsId)
                .NotEmpty()
                .NotNull();
        }
    }
}