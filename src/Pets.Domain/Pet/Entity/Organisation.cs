namespace Pets.Domain.Pet.Entity;

public sealed class Organisation
{
    private Organisation()
    {
    }

    public Organisation(Guid id)
    {
        Id = id;
    }

    /// <summary>
    ///     Идентификатор организации
    /// </summary>
    public Guid Id { get; }

    public static implicit operator Guid(Organisation organisation)
    {
        return organisation.Id;
    }

    public static explicit operator Organisation(Guid id)
    {
        return new Organisation(id);
    }
}