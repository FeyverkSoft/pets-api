using System;

using Pets.Types;

namespace Pets.Queries.Infrastructure.Search.Entity.Search
{
    internal sealed class SearchView
    {
        internal static readonly String SqlCounts = @"
select sum(t.PetCount) as PetCount, sum(t.NewsCount) as NewsCount, sum(t.PageCount) as PageCount from (
  select 
      count(p.Id) as PetCount, 0 as NewsCount, 0 as PageCount
  from `Pet` p
  where 1 = 1
    and p.OrganisationId = @OrganisationId
    and (@Filter is null or p.`Name` like @Filter or p.`MdShortBody` like @Filter or p.`MdBody` like @Filter)
  UNION ALL
  select 
      0, count(n.Id) as NewsCount , 0
  from `News` n
  where 1 = 1
    and n.OrganisationId = @OrganisationId
    and (@Filter is null or n.`MdShortBody` like @Filter or n.`MdBody` like @Filter)
  UNION ALL
  select 
      0, 0, count(p.Id) as PageCount   
  from `Page` p
  where 1 = 1
    and p.OrganisationId = @OrganisationId
    and (@Filter is null or p.`MdBody` like @Filter)
) t
";
        
        /// <summary>
        /// идентификатор найденного объекта
        /// </summary>
        public String Id { get; }

        /// <summary>
        /// Картинка превью к объекту
        /// </summary>
        public String Img { get; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public String Title { get; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public String ShortText { get; }
    }
}