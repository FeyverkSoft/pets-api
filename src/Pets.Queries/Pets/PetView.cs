namespace Pets.Queries.Pets;

using Types;

/// <summary>
///     Инфа о петомце
/// </summary>
public class PetView
{
    public PetView(Guid id, String name, String? beforePhotoLink, String? afterPhotoLink, PetState petState, String mdShortBody, String? mdBody,
        PetType type, PetGender gender, DateTime updateDate, Decimal? animalId)
    {
        (Id, Name, BeforePhotoLink, AfterPhotoLink, PetState, MdShortBody, MdBody, Type, Gender, UpdateDate, AnimalId)
            = (id, name, beforePhotoLink, afterPhotoLink, petState, mdShortBody, mdBody, type, gender, updateDate, animalId);
    }

    public Guid Id { get; }

    /// <summary>
    ///     Имя животного
    /// </summary>
    public String Name { get; }

    /// <summary>
    ///     Ссфлка на фотку до
    /// </summary>
    public String? BeforePhotoLink { get; }

    /// <summary>
    ///     Ссылка на фотку после
    /// </summary>
    public String? AfterPhotoLink { get; }

    /// <summary>
    ///     Состояние животного
    /// </summary>
    public PetState PetState { get; }

    /// <summary>
    ///     Краткое описание в markdown
    /// </summary>
    public String MdShortBody { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; }

    /// <summary>
    ///     Pet type
    ///     Собака/кот/енот/чупакабра
    /// </summary>
    public PetType Type { get; }

    /// <summary>
    ///     Pet type
    ///     Мальчик/Девочка/Неизвестно
    /// </summary>
    public PetGender Gender { get; }

    public DateTime UpdateDate { get; }

    /// <summary>
    ///     Идентификтор чипа пета
    /// </summary>
    public Decimal? AnimalId { get; }
}