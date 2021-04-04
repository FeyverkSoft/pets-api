using System;

using Pets.Types;

namespace Pets.Queries.Infrastructure.Search.Entity.Search
{
    internal record SearchDto
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

        internal static readonly String SqlPets = @"
  select 
      CAST(p.Id as CHAR(36)) as Id,
      'Pet' as Type,
      IFNULL(p.AfterPhotoLink, p.BeforePhotoLink) as Img, 
      p.Name as Title, 
      p.MdShortBody as ShortText
  from `Pet` p
  where 1 = 1
    and p.OrganisationId = @OrganisationId
    and (@Filter is null or p.`Name` like @Filter or p.`MdShortBody` like @Filter or p.`MdBody` like @Filter)
  limit @Limit offset @Offset
";
        internal static readonly String  SqlNews= @"
  select 
      CAST(n.Id as CHAR(36)) as Id,
      'News' as Type,
      n.ImgLink as Img, 
      n.Title as Title, 
      n.MdShortBody as ShortText
  from `News` n
  where 1 = 1
    and n.OrganisationId = @OrganisationId
    and (@Filter is null or n.`MdShortBody` like @Filter or n.`MdBody` like @Filter)
  limit @Limit offset @Offset";
        
        internal static readonly String  SqlPages= @"  
  select 
      p.Id as Id, 
      'Page' as Type,
      p.ImgLink as Img, 
      '' as Title, 
      LEFT(p.MdBody, 384) as ShortText
  from `Page` p
  where 1 = 1
    and p.OrganisationId = @OrganisationId
    and (@Filter is null or p.`MdBody` like @Filter)
  limit @Limit offset @Offset";
        
        /// <summary>
        /// идентификатор найденного объекта
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// Картинка превью к объекту
        /// </summary>
        public String Img { get; set;  }

        /// <summary>
        /// Заголовок
        /// </summary>
        public String Title { get; set;  }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public String ShortText { get; set;  }

        /// <summary>
        /// Тип записи
        /// </summary>
        public SearchObjectType Type { get; }
    }
}