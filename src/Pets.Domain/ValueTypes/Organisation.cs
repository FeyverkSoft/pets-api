namespace Pets.Domain.ValueTypes;

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
    
    public override Boolean Equals(Object? obj)
    {
        if (obj is Organisation org)
        {
            return org?.Id == Id;
        }

        return false;
    }
}