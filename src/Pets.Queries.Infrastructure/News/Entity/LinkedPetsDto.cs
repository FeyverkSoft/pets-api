using System;

namespace Pets.Queries.Infrastructure.News.Entity
{
    internal record LinkedPetsDto //(Guid Id, String Name )
    {
        public Guid Id;
        public String Name;
    };
}