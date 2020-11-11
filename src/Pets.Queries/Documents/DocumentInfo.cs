using System;
using System.IO;

namespace Pets.Queries.Documents
{
    public sealed class DocumentInfo
    {
        public Guid Id { get; }

        public String Filename { get; }

        public String ContentType { get; }

        public Int64 Size { get; }

        public String NonUniqueExtId { get; }

        public MemoryStream Stream { get; }

        public DocumentInfo(Guid id, string contentType, long length, string fileName, string nonUniqueExtId, MemoryStream memoryStream)
        => (Id, Filename, ContentType, Size, NonUniqueExtId, Stream)
            = (id, fileName, contentType, length, nonUniqueExtId, memoryStream);
    }
}
