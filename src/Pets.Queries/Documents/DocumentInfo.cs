namespace Pets.Queries.Documents;

using System.IO;

public sealed record DocumentInfo(Guid Id, String ContentType, Int64 Length, String FileName, String NonUniqueExtId, MemoryStream MemoryStream);