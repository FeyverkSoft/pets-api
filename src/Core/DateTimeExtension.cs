namespace Core;

using System.Globalization;

/// <summary>
/// Набор расширений для преобразований дат
/// </summary>
public static class DateTimeExtension
{
    private static readonly CultureInfo RussianCulture = new ("ru-RU");
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Преобразовать дату в Unix Timestamp
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        return (long)dateTime.ToUniversalTime().Subtract(UnixEpoch).TotalSeconds;
    }

    /// <summary>
    /// Преобразовать дату в Unix Timestamp с миллисекундами
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static ulong ToUnixTimestampWithMilliseconds(this DateTime dateTime)
    {
        return (ulong)(dateTime.ToUniversalTime().Subtract(UnixEpoch).TotalMilliseconds);
    }

    /// <summary>
    /// Обрезать время, только дата
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns>Дата, с нулевым временем в UTC</returns>
    public static DateTime? ToDateOnly(this DateTime? dateTime)
    {
        return dateTime is not null
            ? new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 0, 0, 0,
                DateTimeKind.Unspecified)
            : default(DateTime?);
    }
    
    /// <summary>
    /// Отформатировать дату с учётом русской культуры
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="format"></param>
    /// <returns>Дата отформатированная с учётом русской культуры</returns>
    public static string ToRussianFormat(this DateTime dateTime, string format)
    {
        return dateTime.ToString(format: format, RussianCulture);
    }
}