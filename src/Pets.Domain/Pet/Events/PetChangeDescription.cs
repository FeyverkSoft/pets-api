namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

/// <summary>
///     Cобытие изменения фотки животного в профайле
/// </summary>
public sealed class PetChangeDescription : IEvent
{
    protected PetChangeDescription()
    {
    }

    public PetChangeDescription(Guid petId, String body, DateTime date)
    {
        PetId = petId;
        Date = date;
        Body = body;
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
    public String Body { get; }
}