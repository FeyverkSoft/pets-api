namespace Pets.Infrastructure.Mediatr;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class MediatRDedublicateExecutionAttribute : Attribute
{
    /// <summary>
    ///     Время задержки от повторного вызова
    /// </summary>
    public Int32 ThrottlingTimeMs { get; init; } = 1000;

    /// <summary>
    ///     Название свойства ключа
    /// </summary>
    public String KeyPropertyName { get; init; }

    /// <summary>
    ///     Название ключей
    ///     если задан KeyPropertyName, то это свойство игнорируется
    /// </summary>
    public String[]? KeyPropertyNames { get; init; }
}