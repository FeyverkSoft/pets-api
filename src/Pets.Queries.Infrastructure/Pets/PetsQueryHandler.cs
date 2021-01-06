using System;
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
            var pets = await _db.QueryMultipleAsync(
                new CommandDefinition(
                    commandText: Entity.Pets.PetsView.Sql,
                    parameters: new
                    {
                        Limit = query.Limit,
                        Offset = query.Offset,
                        OrganisationId = query.OrganisationId,
                        PetId = query.PetId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return new Page<PetView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await pets.ReadSingleAsync<Int64>(),
                Items = (await pets.ReadAsync<Entity.Pets.PetsView>()).Select(_ => new PetView(
                    id: _.Id,
                    name: _.Name,
                    beforePhotoLink: _.BeforePhotoLink,
                    afterPhotoLink: _.AfterPhotoLink,
                    petState: _.PetState,
                    mdShortBody: _.MdShortBody,
                    mdBody: _.MdBody,
                    type: _.Type,
                    updateDate: _.UpdateDate
                ))
            };
        }
    }
}