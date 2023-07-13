namespace Pets.Domain.Pet.Entity;

using System.Collections.Generic;

using Events;

using Rabbita.Core.Event;

using Types;

/// <summary>
///     Информация о животном
/// </summary>
public sealed class Pet
{
    public Guid Id { get; }

    /// <summary>
    ///     15 значный номер чипа
    /// </summary>
    public Decimal? AnimalId { get; private set; }

    /// <summary>
    ///     Организация которой принадлежит животное
    /// </summary>
    public Organisation Organisation { get; }

    /// <summary>
    ///     Имя животного
    /// </summary>
    public String Name { get; private set; }

    /// <summary>
    ///     Ссфлка на фотку до
    /// </summary>
    public String? BeforePhotoLink { get; private set; }

    /// <summary>
    ///     Ссылка на фотку после
    /// </summary>
    public String? AfterPhotoLink { get; private set; }

    /// <summary>
    ///     Состояние животного
    /// </summary>
    public PetState PetState { get; private set; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdShortBody { get; private set; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String? MdBody { get; private set; }

    /// <summary>
    ///     Pet type
    ///     Собака/кот/енот/чупакабра
    /// </summary>
    public PetType Type { get; }

    /// <summary>
    ///     Pet type
    ///     Мальчик/Девочка/Неизвестно
    /// </summary>
    public PetGender Gender { get; private set; }

    public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;
    public DateTime CreateDate { get; } = DateTime.UtcNow;

    /// <summary>
    ///     Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
    /// </summary>
    public Guid ConcurrencyTokens { get; } = Guid.NewGuid();

    public List<IEvent> Events { get; } = new();

#pragma warning disable 8618 FOR EF CORE
    internal Pet()
    {
    }
#pragma warning restore 8618

    public Pet(
        Guid petId,
        Organisation organisation,
        String name,
        PetGender gender,
        PetType type,
        PetState petState,
        String? afterPhotoLink,
        String? beforePhotoLink,
        String? mdShortBody,
        String? mdBody,
        DateTime createDate,
        DateTime updateDate,
        Decimal? animalId
    )
    {
        Id = petId;
        Name = name;
        Gender = gender;
        Type = type;
        PetState = petState;
        AfterPhotoLink = afterPhotoLink;
        BeforePhotoLink = beforePhotoLink;
        MdBody = mdBody ?? String.Empty;
        MdShortBody = mdShortBody ?? String.Empty;
        Organisation = organisation;
        CreateDate = createDate;
        UpdateDate = updateDate;
        AnimalId = animalId;
    }

    /// <summary>
    ///     Обновить фотографию профайла пета
    /// </summary>
    /// <param name="beforePhotoLink"></param>
    /// <param name="afterPhotoLink"></param>
    /// <param name="date"></param>
    public void UpdateImg(String? beforePhotoLink, String? afterPhotoLink, DateTime date)
    {
        if (String.IsNullOrEmpty(beforePhotoLink?.Trim()) && String.IsNullOrEmpty(afterPhotoLink?.Trim()))
            return;
        if (BeforePhotoLink == beforePhotoLink && AfterPhotoLink == afterPhotoLink)
            return;

        var before = beforePhotoLink ?? afterPhotoLink;

        if (before is not null && !before.Equals(BeforePhotoLink, StringComparison.InvariantCultureIgnoreCase)) Events.Add(new PetChangeImg(Id, before, date));

        if (afterPhotoLink is not null && !afterPhotoLink.Equals(AfterPhotoLink, StringComparison.InvariantCultureIgnoreCase))
            Events.Add(new PetChangeImg(Id, afterPhotoLink, date));

        AfterPhotoLink = afterPhotoLink;
        BeforePhotoLink = before;
        UpdateDate = date;
    }

    /// <summary>
    ///     Обновить описание и тексты в инфе о пете
    /// </summary>
    /// <param name="mdShortBody"></param>
    /// <param name="mdBody"></param>
    /// <param name="date"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void UpdateDescription(String? mdShortBody, String? mdBody, DateTime date)
    {
        if (MdShortBody is not null && MdShortBody.Equals(mdShortBody)
                                    && MdBody is not null && MdBody.Equals(mdBody))
            return;

        if (String.IsNullOrEmpty(mdShortBody?.Trim()) && String.IsNullOrEmpty(mdBody?.Trim()))
            return;

        if (!String.IsNullOrEmpty(mdShortBody?.Trim()))
        {
            MdShortBody = mdShortBody;
            MdBody ??= mdShortBody;
        }

        if (!String.IsNullOrEmpty(mdBody?.Trim()) && !mdBody.Equals(MdBody))
        {
            MdBody = mdBody;
            Events.Add(new PetChangeDescription(Id, mdBody, date));
        }

        UpdateDate = date;
    }

    public void ChangePetName(String newName, String reason, DateTime updateDate)
    {
        if (!String.IsNullOrEmpty(Name) && Name.Equals(newName))
            return;
        if (String.IsNullOrEmpty(newName?.Trim()))
            throw new InvalidOperationException("Incorrect new name");

        Events.Add(new PetNameChanged(
            Id,
            newName,
            Name,
            reason,
            updateDate)
        );
        Name = newName.Trim();
        UpdateDate = updateDate;
    }

    public void ChangePetGender(PetGender gender, DateTime updateDate)
    {
        if (Gender == gender)
            return;

        Events.Add(new PetGenderChanged(
            Id,
            gender,
            Gender,
            updateDate)
        );

        Gender = gender;
        UpdateDate = updateDate;
    }

    public void ChangePetStatus(PetState state, DateTime updateDate)
    {
        if (PetState == state)
            return;

        Events.Add(new PetStateChanged(
            Id,
            state,
            PetState,
            updateDate)
        );

        PetState = state;
        UpdateDate = updateDate;
    }

    public void ChangeAnimalId(Decimal? animalId, DateTime updateDate)
    {
        if (AnimalId == animalId)
            return;

        Events.Add(new PetAnimalIdChanged(
            Id,
            animalId,
            animalId,
            updateDate)
        );

        AnimalId = animalId;
        UpdateDate = updateDate;
    }
}