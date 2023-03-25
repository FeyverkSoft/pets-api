namespace Pets.Infrastructure.FileStoreService;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

using Domain.Documents;

using Microsoft.AspNetCore.Http;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

public sealed class MongoDocumentRepository : IDocumentRepository
{
    private readonly IGridFSBucket _gridFs;
    private readonly SHA512 _hasher = SHA512.Create();

    public MongoDocumentRepository(IGridFSBucket gridFs)
    {
        _gridFs = gridFs;
    }

    /// <summary>
    ///     Сохранить файл в стор
    /// </summary>
    /// <param name="file">файл</param>
    /// <param name="cancellationToken">токен признака отмены</param>
    /// <returns>Идентификатор сохранённого файла</returns>
    public async Task<String> SaveFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var hash = await GetHash(file, cancellationToken);
        var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Metadata["hash"], hash);
        var existsFiles = await _gridFs.FindAsync(filter, cancellationToken: cancellationToken);
        var existsFile = await existsFiles.FirstOrDefaultAsync(cancellationToken);
        if (existsFile != null)
            return existsFile.Metadata["FileName"].AsString;

        var fileName = Guid.NewGuid().ToString();
        var metaData = new Dictionary<String, Object>
        {
            ["ContentType"] = file.ContentType,
            ["length"] = file.Length,
            ["NonUniqueExtId"] = file.FileName,
            ["FileName"] = fileName,
            ["hash"] = hash
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

    private async Task<BsonValue> GetHash(IFormFile file, CancellationToken cancellationToken)
    {
        var data = _hasher.ComputeHash(file.OpenReadStream());
        return data.Aggregate(String.Empty, (current, t) => current + t.ToString("x2"));
    }
}