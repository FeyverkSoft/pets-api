namespace Pets.DB.Migrations.Entities;

using System;
using System.Collections.Generic;

using Types;

/// <summary>
///     Информация о животном
/// </summary>
internal sealed class Pet
{
    public Guid Id { get; }

    /// <summary>
    ///     Идентификатор организации которой принадлежит животное
    /// </summary>
    public Guid OrganisationId { get; }

    public Organisation Organisation { get; }

    /// <summary>
    ///     15 значный номер чипа
    /// </summary>
    public Decimal? AnimalId { get; }

    /// <summary>
    ///     Имя животного
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String Name { get; }

    /// <summary>
    ///     Ссфлка на фотку до
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String? BeforePhotoLink { get; }

    /// <summary>
    ///     Ссылка на фотку после
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String? AfterPhotoLink { get; }

    /// <summary>
    ///     Состояние животного
    /// </summary>
    /// <typeparamref name="NVARCHAR(64) NOT NULL" />
    public PetState PetState { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    /// <typeparamref name="NVARCHAR(512) NOT NULL" />
    public String MdShortBody { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    /// <typeparamref name="NVARCHAR(10240) NOT NULL" />
    public String MdBody { get; }

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
    public DateTime CreateDate { get; }

    /// <summary>
    ///     Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
    /// </summary>
    public Guid ConcurrencyTokens { get; }

    public IEnumerable<NewsPets> PetNews { get; }
}