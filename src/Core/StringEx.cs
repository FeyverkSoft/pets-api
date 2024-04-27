namespace Core;

public static class StringEx
{
    /// <summary>
    /// Сравнение строк без учета регистра и культуры
    /// </summary>
    public static Boolean IgnoreCaseEquals(this String? str, String? value)
    {
        if (str is null && value is null)
            return true;
        return str?.Equals(value, StringComparison.InvariantCultureIgnoreCase) == true;
    }
}