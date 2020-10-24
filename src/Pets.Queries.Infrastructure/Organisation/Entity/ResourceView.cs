using System;

using Pets.Types;

namespace Pets.Queries.Infrastructure.Organisation.Entity
{
    internal sealed class ResourceView
    {
        internal static readonly String Sql = @"
select 
    p.OrganisationId, 
    p.ImgLink,
    p.MdBody,
    p.Title,
    p.State
from `Resource` p
where 1 = 1
and OrganisationId = @OrganisationId
";

        /// <summary>
        /// Id организации
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Ссылка на значёк
        /// </summary>
        public String? ImgLink { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String? MdBody { get; }

        /// <summary>
        /// Заголовок в markdown
        /// </summary>
        public String? Title { get; }

        public ResourceState State { get; }
    }
}