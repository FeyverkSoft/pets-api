namespace Pets.Queries.Documents;

using System.IO;

public sealed class DocumentInfo
{
    public DocumentInfo(Guid id, String contentType, Int64 length, String fileName, String nonUniqueExtId, MemoryStream memoryStream)
    {
        (Id, Filename, ContentType, Size, NonUniqueExtId, Stream)
            = (id, fileName, contentType, length, nonUniqueExtId, memoryStream);
    }

    public Guid Id { get; }

    public String Filename { get; }

    public String ContentType { get; }

    public Int64 Size { get; }

    public String NonUniqueExtId { get; }

    public MemoryStream Stream { get; }
}