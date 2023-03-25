namespace Pets.Types;

public enum PetState
{
    /// <summary>
    ///     Живой
    /// </summary>
    Alive,

    /// <summary>
    ///     Мёртвый
    /// </summary>
    Death,

    /// <summary>
    ///     Пристроенный
    /// </summary>
    Adopted,

    /// <summary>
    ///     SOS
    /// </summary>
    Critical,

    /// <summary>
    ///     Ищит семью
    /// </summary>
    Wanted,

    /// <summary>
    ///     Питомцы которые живут в приюте на постоянной основе
    /// </summary>
    OurPets
}