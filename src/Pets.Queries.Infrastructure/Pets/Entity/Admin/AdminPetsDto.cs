﻿namespace Pets.Queries.Infrastructure.Pets.Entity.Admin;

using System;

using Types;

internal sealed class AdminPetsDto
{
    internal static readonly String Sql = @"
select 
    count(p.Id)   
from `Pet` p
where 1 = 1
and OrganisationId = @OrganisationId
and (@PetId is null or p.Id = @PetId)     
and (@Filter is null or p.`Name` like @Filter or p.`MdShortBody` like @Filter)
and (p.`PetState` IN @Status)
and (p.`Type` IN @Types)     
and (p.`Gender` IN @Genders);

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
and (@PetId is null or p.Id = @PetId)
and (@Filter is null or p.`Name` like @Filter or p.`MdShortBody` like @Filter)
and (p.`PetState` IN @Status)
and (p.`Type` IN @Types) 
and (p.`Gender` IN @Genders)
order by p.UpdateDate desc 
limit @Limit offset @Offset
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