namespace Pets.Types;

public enum NewsState
{
    /// <summary>
    /// Новость видна пользователю
    /// </summary>
    Active,
    /// <summary>
    /// Новость удалена
    /// </summary>
    Delete,
    /// <summary>
    /// Новость скрыта для отображения / черновик
    /// </summary>
    Draft,
    /// <summary>
    /// Новость закреплена на главной
    /// </summary>
    Pinned,
}