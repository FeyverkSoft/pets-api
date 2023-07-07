namespace Pets.Queries.Pets;

using Types;

/// <summary>
///     Инфа о петомце
/// </summary>
/// <param name="Id"></param>
/// <param name="Name">Имя животного</param>
/// <param name="BeforePhotoLink">Ссылка на фотку до</param>
/// <param name="AfterPhotoLink">Ссылка на фотку после</param>
/// <param name="PetState">Состояние животного</param>
/// <param name="MdShortBody">Краткое описание в markdown</param>
/// <param name="MdBody">Тело в markdown</param>
/// <param name="Type">Pet type  Собака/кот/енот/чупакабра</param>
/// <param name="Gender">Pet type   Мальчик/Девочка/Неизвестно</param>
/// <param name="UpdateDate"></param>
/// <param name="AnimalId">Идентификтор чипа пета</param>
public sealed record PetView(Guid Id, String Name, String? BeforePhotoLink, String? AfterPhotoLink, PetState PetState, String MdShortBody, String? MdBody,
    PetType Type, PetGender Gender, DateTime UpdateDate, Decimal? AnimalId);