namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

/// <summary>
///     Cобытие изменения фотки животного в профайле
/// </summary>
public sealed class PetChangeImg : IEvent
{
    protected PetChangeImg()
    {
    }

    public PetChangeImg(Guid petId, String link, DateTime date)
    {
        PetId = petId;
        Date = date;
        Link = link;
    }

    /// <summary>
    ///     Идентификатор питомца
    /// </summary>
    public Guid PetId { get; }

    /// <summary>
    ///     Дата события
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    ///     Ссылка на новую фотку
    /// </summary>
    public String Link { get; }
}