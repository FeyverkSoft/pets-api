namespace Pets.Queries.Infrastructure.News.Entity;

using System;

internal record LinkedPetsDto //(Guid Id, String Name )
{
    public Guid Id;
    public String Name;
}