using System;

using Pets.Types;

namespace Pets.Queries.Infrastructure.Pets.Entity.Pets
{
    internal sealed class PetsView
    {
        internal static readonly String Sql = @"
select 
    p.Id,
    p.Name, 
    p.BeforePhotoLink,
    p.AfterPhotoLink,
    p.PetState,
    p.MdBody,
    p.Type,
    p.UpdateDate
from `Pets` p
where 1 = 1
order by p.UpdateDate desc 
";

        public Guid Id { get; }

        /// <summary>
        /// Имя животного
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Ссфлка на фотку до
        /// </summary>
        public String BeforePhotoLink { get; }

        /// <summary>
        /// Ссылка на фотку после
        /// </summary>
        public String? AfterPhotoLink { get; }

        /// <summary>
        /// Состояние животного
        /// </summary>
        public PetState PetState { get; }

        /// <summary>
        /// Тело в markdown
        /// </summary>
        public String MdBody { get; }

        /// <summary>
        /// Pet type
        /// Собака/кот/енот/чупакабра
        /// </summary>
        public PetType Type { get; }

        public DateTime UpdateDate { get; }
    }
}