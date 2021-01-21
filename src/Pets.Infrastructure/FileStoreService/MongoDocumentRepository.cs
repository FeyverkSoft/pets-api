using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using MongoDB.Bson;
using MongoDB.Driver.GridFS;

using Pets.Domain.Documents;

namespace Pets.Infrastructure.FileStoreService
{
    public sealed class MongoDocumentRepository : IDocumentRepository
    {
        private readonly IGridFSBucket _gridFs;

        public MongoDocumentRepository(IGridFSBucket gridFs)
        {
            _gridFs = gridFs;
        }

        /// <summary>
        /// Сохранить файл в стор
        /// </summary>
        /// <param name="file">файл</param>
        /// <param name="cancellationToken">токен признака отмены</param>
        /// <returns>Идентификатор сохранённого файла</returns>
        public async Task<String> SaveFileAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid().ToString();
            var metaData = new Dictionary<String, Object>
            {
                ["ContentType"] = file.ContentType,
                ["length"] = file.Length,
                ["NonUniqueExtId"] = file.Name,
                ["FileName"] = fileName,
            };
            await _gridFs.UploadFromStreamAsync(
                fileName,
                file.OpenReadStream(),
                new GridFSUploadOptions
                {
                    Metadata = new BsonDocument(metaData)
                }, cancellationToken);
            return fileName;
        }
    }
}