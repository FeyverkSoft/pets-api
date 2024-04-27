﻿namespace Pets.Domain.News.Entity;

using System.Collections.Generic;
using System.Linq;

using Events;

using Rabbita.Core.Event;

using ValueTypes;

/// <summary>
/// Новость
/// </summary>
public sealed record News
{
    public Guid Id { get; private set; }

    /// <summary>
    ///     Организация которой принадлежит новость
    /// </summary>
    public Organisation Organisation { get; private set; }

    /// <summary>
    /// Заголовок новости
    /// </summary>
    public String Title { get; private set; }

    /// <summary>
    /// Ссылка на картинку шапки
    /// </summary>
    public String? ImgLink { get; private set; }

    /// <summary>
    /// Предпросмотр новости в markdown
    /// </summary>
    public String MdShortBody { get; private set; }

    /// <summary>
    /// Тело в markdown
    /// </summary>
    public String MdBody { get; private set; }

    /// <summary>
    /// Дата публикации новости
    /// </summary>
    public DateTime CreateDate { get; private set; }

    /// <summary>
    /// Связанные животные с новостью
    /// </summary>
    public ICollection<LinkedPets> LinkedPets { get; private set; }

    /// <summary>
    /// Теги новости
    /// </summary>
    public ICollection<String> Tags { get; private set; }

    public List<IEvent> Events { get; } = new();

    /// <summary>
    ///     Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросах
    /// </summary>
    public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Дата обновления новости
    /// </summary>
    public DateTime UpdateDate { get; private set; }

    /// <summary>
    /// Порождающий метод объектов типа News
    /// </summary>
    /// <param name="id"></param>
    /// <param name="organisation"></param>
    /// <param name="title"></param>
    /// <param name="mdShortBody"></param>
    /// <param name="mdBody"></param>
    /// <param name="imgLink"></param>
    /// <param name="createDate"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static News Create(Guid id, Organisation organisation, String title, String mdShortBody, String mdBody,
        String? imgLink, DateTime createDate)
    {
        return new News
        {
            Id = id,
            Organisation = organisation,
            Title = title ?? throw new ArgumentNullException(nameof(title)),
            MdShortBody = mdShortBody ?? throw new ArgumentNullException(nameof(mdShortBody)),
            MdBody = mdBody ?? throw new ArgumentNullException(nameof(mdBody)),
            ImgLink = imgLink,
            CreateDate = createDate,
        };
    }

    public void AddTags(ICollection<String?>? requestTags, DateTime date)
    {
        var newTags = requestTags?
            .Where(t => t is not null)
            .Distinct(StringComparer.InvariantCultureIgnoreCase) ?? new List<String?>();
        var tagsToAdd = Tags.Except(newTags, StringComparer.InvariantCultureIgnoreCase);

        foreach (var t in tagsToAdd)
        {
            Tags.Add(t);
        }

        UpdateDate = date;
    }


    public void AddPets(IEnumerable<Pet?> pets, DateTime date)
    {
        var newPets = pets?
            .Where(t => t is not null)
            .DistinctBy(_ => _.Id);
        var petIds = newPets.Select(_ => _.Id).Except(LinkedPets.Select(_ => _.PetId));
        foreach (var petId in petIds)
        {
            var p = newPets.FirstOrDefault(_ => _.Id == petId);
            if (p is null)
                continue;
            Events.Add(new PetLinkToNews(
                PetId: Id,
                PetName: p.Name,
                NewsId: Id,
                NewsTitle: Title,
                Date: date)
            );
        }

        UpdateDate = date;
    }
};

/// <summary>
///     Список связанных с новостью животных
/// </summary>
/// <param name="PetId">Идентификатор животного</param>
/// <param name="Name">Имя животного</param>
public sealed record LinkedPets(Guid PetId, String Name)
{
    public static LinkedPets Create(Guid petId, String name)
    {
        return new LinkedPets(petId, name ?? throw new ArgumentNullException(nameof(name)));
    }

    public IEnumerable<News> News { get; private set; }
};