namespace Pets.Domain.Pet.Events;

using Rabbita.Core.Event;

/// <summary>
/// Уведомление об изменении имени питомца
/// </summary>
/// <param name="PetId">Идентификатор питомца</param>
/// <param name="Name">Новое имя пета</param>
/// <param name="OldName"> Старое имя пета</param>
/// <param name="Reason"> Причина смены имени</param>
/// <param name="Date"> Дата события</param>
public record PetNameChanged(Guid PetId, String Name, String OldName, String Reason, DateTime Date) : IEvent;