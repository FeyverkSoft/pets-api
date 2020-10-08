using System;

namespace Pets.Queries.Pets
{
    public sealed class GetPetsQuery : PageQuery<PetView>
    {
        /// <summary>
        /// некий фильтр, пока что не понятно какие параметры
        /// </summary>
        public String? Filter { get; }

        public GetPetsQuery(Int32 offset=0, Int32 limit=8, String? filter=null)
            :base(offset, limit)
            => (Filter)
            =  (filter);
    }
}
