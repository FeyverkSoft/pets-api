namespace Pets.Api.Models.Public.Img;

public sealed class UploadFileView
{
    public UploadFileView(String fileId)
    {
        FileId
            = fileId;
    }

    /// <summary>
    ///     идентификатор картинки
    /// </summary>
    public String FileId { get; }
}