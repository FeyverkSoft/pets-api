namespace Pets.Queries.News;

using System.Collections.Generic;

public sealed class NewsView
{
    public NewsView(Guid id, String title, String imgLink, String mdShortBody, String mdBody, DateTime createDate, ICollection<LinkedPetsView> linkedPets,
        ICollection<String> tags)
    {
        (Id, Title, ImgLink, MdShortBody, MdBody, CreateDate, LinkedPets, Tags)
            = (id, title, imgLink, mdShortBody, mdBody, createDate, linkedPets, tags);
    }

    /// <summary>
    ///     Идентификатор новости
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Заголовок новости
    /// </summary>
    public String Title { get; }

    /// <summary>
    ///     Теги новости
    /// </summary>
    public ICollection<String> Tags { get; }

    /// <summary>
    ///     Ссылка на картинку шапки
    /// </summary>
    public String ImgLink { get; }

    /// <summary>
    ///     Предпросмотр новости в markdown
    /// </summary>
    public String MdShortBody { get; }

    /// <summary>
    ///     Тело в markdown
    /// </summary>
    public String MdBody { get; }

    /// <summary>
    ///     Дата публикации новости
    /// </summary>
    public DateTime CreateDate { get; }

    public ICollection<LinkedPetsView> LinkedPets { get; }
}