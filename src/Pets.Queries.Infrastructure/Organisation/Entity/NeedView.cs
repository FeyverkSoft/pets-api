using System;

using Pets.Types;

namespace Pets.Queries.Infrastructure.Organisation.Entity
{
    internal sealed class NeedView
    {
        internal static readonly String Sql = @"
select 
    n.OrganisationId, 
    n.ImgLinks,
    n.MdBody,
    n.NeedState
from `Need` n
where 1 = 1
and OrganisationId = @OrganisationId
and n.`NeedState` IN @State
";

        /// <summary>
        /// Id организации
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Ссылка на картинки
        /// </summary>
        public String? ImgLinks { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Заголовок в markdown
        /// </summary>
        public String? Title { get; }

        public NeedState State { get; }
    }
}