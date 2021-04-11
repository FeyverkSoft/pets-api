using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Pets.Queries.Pets;
using Pets.Types;

using Query.Core;

namespace Pets.Queries.Infrastructure.Pets
{
    public sealed class PetsQueryHandler :
        IQueryHandler<GetPetsQuery, Page<PetView>>,
        IQueryHandler<GetPetQuery, PetView?>
    {
        private static readonly List<PetState> DefaultPetStatuses = Enum.GetNames(typeof(PetState)).Select(Enum.Parse<PetState>).ToList();
        private static readonly List<PetGender> DefaultPetGenders = Enum.GetNames(typeof(PetGender)).Select(Enum.Parse<PetGender>).ToList();

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
                        Status = (query.PetStatuses.Any() ? query.PetStatuses : DefaultPetStatuses).Select(_ => _.ToString()),
                        Genders = (query.Genders.Any() ? query.Genders : DefaultPetGenders).Select(_ => _.ToString()),
                        Filter = query.Filter == null ? null : $"%{query.Filter}%",
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
                    gender: _.Gender,
                    updateDate: _.UpdateDate
                ))
            };
        }

        public async Task<PetView?> Handle(GetPetQuery query, CancellationToken cancellationToken)
        {
            var pet = await _db.QuerySingleOrDefaultAsync<Entity.Pets.PetView>(
                new CommandDefinition(
                    commandText: Entity.Pets.PetView.Sql,
                    parameters: new
                    {
                        OrganisationId = query.OrganisationId,
                        PetId = query.PetId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            if (pet is null)
                return null;

            return new PetView(
                id: pet.Id,
                name: pet.Name,
                beforePhotoLink: pet.BeforePhotoLink,
                afterPhotoLink: pet.AfterPhotoLink,
                petState: pet.PetState,
                mdShortBody: pet.MdShortBody,
                mdBody: pet.MdBody,
                type: pet.Type,
                gender: pet.Gender,
                updateDate: pet.UpdateDate
            );
        }
    }
}