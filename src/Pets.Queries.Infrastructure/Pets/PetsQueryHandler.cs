using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Pets.Queries.Pets;

using Query.Core;

namespace Pets.Queries.Infrastructure.Pets
{
    public sealed class PetsQueryHandler : IQueryHandler<GetPetsQuery, Page<PetView>>

    {
        private readonly IDbConnection _db;

        public PetsQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<Page<PetView>> IQueryHandler<GetPetsQuery, Page<PetView>>.Handle(GetPetsQuery query,
            CancellationToken cancellationToken)
        {
            var pets = await _db.QueryAsync<Entity.Pets.PetsView>(
                new CommandDefinition(
                    commandText: Entity.Pets.PetsView.Sql,
                    parameters: new
                    {
                        Limit = query.Limit,
                        Offset = query.Offset
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return new Page<PetView>();
        }
    }
}