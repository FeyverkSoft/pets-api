namespace Pets.Queries.Infrastructure.Documents;

using System.IO;

using MongoDB.Driver;
using MongoDB.Driver.GridFS;

using Queries.Documents;

public sealed class DocumentsQueryHandler :
    IRequestHandler<GetImgQuery, DocumentInfo?>
{
    private readonly IGridFSBucket _gridFs;

    public DocumentsQueryHandler(IGridFSBucket gridFs)
    {
        _gridFs = gridFs;
    }

    async Task<DocumentInfo?> IRequestHandler<GetImgQuery, DocumentInfo?>.Handle(GetImgQuery query, CancellationToken cancellationToken)
    {
        var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Filename, query.ImageId.ToString());
        var fileInfo = (await _gridFs.FindAsync(filter, null, cancellationToken)).FirstOrDefault();

        if (fileInfo == null)
            return null;
        var memstream = new MemoryStream();
        await _gridFs.DownloadToStreamAsync(fileInfo.Id, memstream, cancellationToken: cancellationToken);

        return new DocumentInfo(
            Id: query.ImageId,
            MemoryStream: memstream,
            ContentType: fileInfo.Metadata["ContentType"].ToString(),
            Length: fileInfo.Length,
            FileName: fileInfo.Metadata.Contains("FileName") ? fileInfo.Metadata["FileName"].ToString() : "",
            NonUniqueExtId: fileInfo.Metadata.Contains("NonUniqueExtId") ? fileInfo.Metadata["NonUniqueExtId"].ToString() : ""
        );
    }
}