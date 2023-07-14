namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

/// <summary>
/// Cобытие изменения фотки животного в профайле
/// </summary>
/// <param name="PetId">Идентификатор питомца</param>
/// <param name="Link">Ссылка на новую фотку</param>
/// <param name="Date">Дата события</param>
public sealed record PetChangeImg(Guid PetId, String Link, DateTime Date) : IEvent;