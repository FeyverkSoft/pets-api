namespace Pets.Types;

public enum ResourceState
{
    /// <summary>
    ///     Больше не требуется
    /// </summary>
    Complete,

    /// <summary>
    ///     Активен сбор
    /// </summary>
    Active,

    /// <summary>
    ///     Срочно необходимо
    /// </summary>
    Urgently
}