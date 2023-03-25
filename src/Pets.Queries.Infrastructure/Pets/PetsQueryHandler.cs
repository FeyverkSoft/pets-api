namespace Pets.Queries.Infrastructure.Pets;

using System;
using System.Collections.Generic;
using System.Linq;

using Entity.Pets;

using Queries.Pets;

using Types;

using PetView = global::Pets.Queries.Pets.PetView;

public sealed class PetsQueryHandler :
    IRequestHandler<GetPetsQuery, Page<PetView>>,
    IRequestHandler<GetPetQuery, PetView?>
{
    private static readonly List<PetState> DefaultPetStatuses = Enum.GetNames(typeof(PetState)).Select(Enum.Parse<PetState>).ToList();
    private static readonly List<PetGender> DefaultPetGenders = Enum.GetNames(typeof(PetGender)).Select(Enum.Parse<PetGender>).ToList();

    private readonly IDbConnection _db;

    public PetsQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    public async Task<PetView?> Handle(GetPetQuery query, CancellationToken cancellationToken)
    {
        var pet = await _db.QuerySingleOrDefaultAsync<Entity.Pets.PetView>(
            new CommandDefinition(
                Entity.Pets.PetView.Sql,
                new
                {
                    query.OrganisationId, query.PetId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));

        if (pet is null)
            return null;

        return new PetView(
            pet.Id,
            pet.Name,
            pet.BeforePhotoLink,
            pet.AfterPhotoLink,
            pet.PetState,
            pet.MdShortBody,
            pet.MdBody,
            pet.Type,
            pet.Gender,
            pet.UpdateDate,
            pet.AnimalId
        );
    }

    async Task<Page<PetView>> IRequestHandler<GetPetsQuery, Page<PetView>>.Handle(GetPetsQuery query,
        CancellationToken cancellationToken)
    {
        var pets = await _db.QueryMultipleAsync(
            new CommandDefinition(
                PetsView.Sql,
                new
                {
                    query.Limit,
                    query.Offset,
                    query.OrganisationId,
                    query.PetId,
                    Status = (query.PetStatuses.Any() ? query.PetStatuses : DefaultPetStatuses).Select(_ => _.ToString()),
                    Genders = (query.Genders.Any() ? query.Genders : DefaultPetGenders).Select(_ => _.ToString()),
                    Filter = query.Filter == null ? null : $"%{query.Filter}%"
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
        return new Page<PetView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await pets.ReadSingleAsync<Int64>(),
            Items = (await pets.ReadAsync<PetsView>()).Select(_ => new PetView(
                _.Id,
                _.Name,
                _.BeforePhotoLink,
                _.AfterPhotoLink,
                _.PetState,
                _.MdShortBody,
                _.MdBody,
                _.Type,
                _.Gender,
                _.UpdateDate,
                _.AnimalId
            ))
        };
    }
}