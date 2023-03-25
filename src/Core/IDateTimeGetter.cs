namespace Core
{
    /// <summary>
    ///     Обёртка над датой и временем для удобства покрытия тестами
    /// </summary>
    public interface IDateTimeGetter
    {
        /// <summary>
        ///     Получить текущую дату и время
        /// </summary>
        /// <returns></returns>
        public DateTime Get()
        {
            return DateTime.UtcNow;
        }
    }

    public sealed class DefaultDateTimeGetter : IDateTimeGetter
    {
    }
}