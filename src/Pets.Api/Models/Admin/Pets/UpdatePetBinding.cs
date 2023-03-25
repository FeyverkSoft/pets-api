namespace Pets.Api.Models.Admin.Pets;

public sealed class UpdatePetBinding
{
    /// <summary>
    ///     Ссфлка на фотку до
    /// </summary>
    public String? BeforePhotoLink { get; set; }

    /// <summary>
    ///     Ссылка на фотку после
    /// </summary>
    public String? AfterPhotoLink { get; set; }

    /// <summary>
    ///     Краткое описание в markdown
    /// </summary>
    public String? MdShortBody { get; set; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; set; }

    public sealed class UpdatePetBindingValidator : AbstractValidator<UpdatePetBinding>
    {
        public UpdatePetBindingValidator()
        {
            RuleFor(_ => _.AfterPhotoLink)
                .MaximumLength(512);

            RuleFor(_ => _.BeforePhotoLink)
                .MaximumLength(512);

            RuleFor(_ => _.MdBody)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10240);

            RuleFor(_ => _.MdShortBody)
                .NotNull()
                .NotEmpty()
                .MaximumLength(512);
        }
    }
}