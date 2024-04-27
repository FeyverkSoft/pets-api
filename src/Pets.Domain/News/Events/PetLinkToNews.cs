namespace Pets.Domain.News.Events;

using Rabbita.Core.Event;

public record PetLinkToNews(Guid PetId, String PetName, Guid NewsId, String NewsTitle, DateTime Date) : IEvent;