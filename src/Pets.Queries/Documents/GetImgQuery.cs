using Query.Core;

using System;

namespace Pets.Queries.Documents
{
    /// <summary>
    /// Запрос на получение картинки по id картинки
    /// </summary>
    public sealed class GetImgQuery : IQuery<DocumentInfo?>
    {
        /// <summary>
        /// Картинка
        /// </summary>
        public Guid ImageId { get; }

        public GetImgQuery(Guid imageId)
            => (ImageId)
                = (imageId);
    }
}
