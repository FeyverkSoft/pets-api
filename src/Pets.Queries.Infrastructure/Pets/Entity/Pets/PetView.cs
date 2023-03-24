namespace Pets.Queries.Infrastructure.Pets.Entity.Pets;

using System;

using Types;

internal sealed record PetView
{
    internal static readonly String Sql = @"
select 
    p.Id,
    p.Name, 
    p.BeforePhotoLink,
    p.AfterPhotoLink,
    p.PetState,
    p.MdShortBody,
    p.MdBody,
    p.Type,
    p.Gender,
    p.AnimalId,
    p.UpdateDate
from `Pet` p
where 1 = 1
and p.OrganisationId = @OrganisationId
and p.Id = @PetId
";

    public Guid Id { get; }

    /// <summary>
    ///     Идентификтор чипа пета
    /// </summary>
    public Decimal? AnimalId { get; }

    /// <summary>
    ///     Имя животного
    /// </summary>
    public String Name { get; }

    /// <summary>
    ///     Ссфлка на фотку до
    /// </summary>
    public String BeforePhotoLink { get; }

    /// <summary>
    ///     Ссылка на фотку после
    /// </summary>
    public String? AfterPhotoLink { get; }

    /// <summary>
    ///     Состояние животного
    /// </summary>
    public PetState PetState { get; }

    /// <summary>
    ///     Тело в markdown
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
}