using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Pets.Domain.Documents
{
    /// <summary>
    /// Интерфейс для работы с файловым хранилищем
    /// </summary>
    public interface IDocumentRepository
    {
        /// <summary>
        /// Сохранить файл в стор
        /// </summary>
        /// <param name="file">файл</param>
        /// <param name="cancellationToken">токен признака отмены</param>
        /// <returns>Идентификатор сохранённого файла</returns>
        Task<String> SaveFileAsync(IFormFile file, CancellationToken cancellationToken);
    }
}