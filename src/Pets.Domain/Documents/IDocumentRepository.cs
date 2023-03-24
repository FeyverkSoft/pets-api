namespace Pets.Domain.Documents;

using Microsoft.AspNetCore.Http;

/// <summary>
///     Интерфейс для работы с файловым хранилищем
/// </summary>
public interface IDocumentRepository
{
    /// <summary>
    ///     Сохранить файл в стор
    /// </summary>
    /// <param name="file">файл</param>
    /// <param name="cancellationToken">токен признака отмены</param>
    /// <returns>Идентификатор сохранённого файла</returns>
    Task<String> SaveFileAsync(IFormFile file, CancellationToken cancellationToken);
}