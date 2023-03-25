namespace Pets.Api.Models.Admin.Pets;

using Types;

public sealed class CreatePetBinding
{
    /// <summary>
    ///     Идентификатор питомца
    /// </summary>
    public Guid PetId { get; set; }

    /// <summary>
    ///     15 значный ID чипа
    /// </summary>
    public Decimal? AnimalId { get; set; }

    /// <summary>
    ///     Имя пета
    /// </summary>
    public String Name { get; set; }

    /// <summary>
    ///     Ссфлка на фотку до
    /// </summary>
    public String? BeforePhotoLink { get; set; }

    /// <summary>
    ///     Ссылка на фотку после
    /// </summary>
    public String? AfterPhotoLink { get; set; }

    /// <summary>
    ///     Состояние животного
    /// </summary>
    public PetState PetState { get; set; }

    /// <summary>
    ///     Краткое описание в markdown
    /// </summary>
    public String? MdShortBody { get; set; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; set; }

    /// <summary>
    ///     Pet type
    ///     Собака/кот/енот/чупакабра
    /// </summary>
    public PetType Type { get; set; }

    /// <summary>
    ///     Пол питомца
    /// </summary>
    public PetGender PetGender { get; set; } = PetGender.Unset;

    public sealed class CreatePetBindingValidator : AbstractValidator<CreatePetBinding>
    {
        public CreatePetBindingValidator()
        {
            RuleFor(_ => _.PetId)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(512);

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