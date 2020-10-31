using System;
using System.Collections.Generic;

using Pets.Types;

namespace Pets.Queries.Organisation
{
    public sealed class NeedView
    {
        /// <summary>
        /// Ссылка на фотографию материала
        /// </summary>
        public IEnumerable<String?> ImgsLink { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Видимость заказа
        /// </summary>
        public NeedState State { get; }

        public NeedView(IEnumerable<String?> imgsLink, String? mdBody, NeedState state)
            => (ImgsLink,  MdBody, State)
                = (imgsLink,  mdBody, state);
    }
}