﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Pets.Queries.Documents;

using Query.Core;

using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Pets.Queries.Infrastructure.Documents
{
    public sealed class DocumentsQueryHandler :
        IQueryHandler<GetImgQuery, DocumentInfo?>
    {
        private readonly IGridFSBucket _gridFs;
        public DocumentsQueryHandler(IGridFSBucket gridFs)
        {
            _gridFs = gridFs;
        }

        async Task<DocumentInfo?> IQueryHandler<GetImgQuery, DocumentInfo?>.Handle(GetImgQuery query, CancellationToken cancellationToken)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Filename, query.ImageId.ToString());
            var fileInfo = (await _gridFs.FindAsync(filter, null, cancellationToken)).FirstOrDefault();

            if (fileInfo == null)
                return null;
            var memstream = new MemoryStream();
            await _gridFs.DownloadToStreamAsync(fileInfo.Id, memstream, cancellationToken: cancellationToken);

            return new DocumentInfo(
                id: query.ImageId,
                memoryStream: memstream,
                contentType: fileInfo.Metadata["ContentType"].ToString(),
                length: fileInfo.Length,
                fileName: fileInfo.Metadata.Contains("FileName") ? fileInfo.Metadata["FileName"].ToString() : "",
                nonUniqueExtId: fileInfo.Metadata.Contains("NonUniqueExtId") ? fileInfo.Metadata["NonUniqueExtId"].ToString() : ""
            );
        }
    }
}