namespace Pets.Queries.Infrastructure.Pets;

using System;
using System.Collections.Generic;
using System.Linq;

using Entity.Admin;
using Entity.Pets;

using Queries.Pets;
using Queries.Pets.Admin;

using Types;

using PetView = global::Pets.Queries.Pets.PetView;

public sealed class PetsQueryHandler :
    IRequestHandler<GetPetsQuery, Page<PetView>>,
    IRequestHandler<GetPetQuery, PetView?>,
    IRequestHandler<AdminGetPetsQuery, Page<PetView>>
    
{
    private static readonly List<PetState> DefaultPetStatuses = Enum.GetNames(typeof(PetState)).Select(Enum.Parse<PetState>).ToList();
    private static readonly List<PetGender> DefaultPetGenders = Enum.GetNames(typeof(PetGender)).Select(Enum.Parse<PetGender>).ToList();
    private static readonly List<PetType> DefaultPetTypes = Enum.GetNames(typeof(PetType)).Select(Enum.Parse<PetType>).ToList();

    private readonly IDbConnection _db;

    public PetsQueryHandler(IDbConnection db)
    {
        _db = db;
    }

    public async Task<PetView?> Handle(GetPetQuery query, CancellationToken cancellationToken)
    {
        var pet = await _db.QuerySingleOrDefaultAsync<Entity.Pets.PetDto>(
            new CommandDefinition(
                Entity.Pets.PetDto.Sql,
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
            Id: pet.Id,
            Name: pet.Name,
            BeforePhotoLink: pet.BeforePhotoLink,
            AfterPhotoLink: pet.AfterPhotoLink,
            PetState: pet.PetState,
            MdShortBody: pet.MdShortBody,
            MdBody: pet.MdBody,
            Type: pet.Type,
            Gender: pet.Gender,
            UpdateDate: pet.UpdateDate,
            AnimalId: pet.AnimalId
        );
    }

    async Task<Page<PetView>> IRequestHandler<GetPetsQuery, Page<PetView>>.Handle(GetPetsQuery query,
        CancellationToken cancellationToken)
    {
        var pets = await _db.QueryMultipleAsync(
            new CommandDefinition(
                PetsDto.Sql,
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
            Items = (await pets.ReadAsync<PetsDto>()).Select(_ => new PetView(
                Id: _.Id,
                Name: _.Name,
                BeforePhotoLink: _.BeforePhotoLink,
                AfterPhotoLink: _.AfterPhotoLink,
                PetState: _.PetState,
                MdShortBody: _.MdShortBody,
                MdBody: _.MdBody,
                Type: _.Type,
                Gender: _.Gender,
                UpdateDate: _.UpdateDate,
                AnimalId: _.AnimalId
            ))
        };
    }

    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Page<PetView>> Handle(AdminGetPetsQuery query, CancellationToken cancellationToken)
    {
        var pets = await _db.QueryMultipleAsync(
            new CommandDefinition(
                AdminPetsDto.Sql,
                new
                {
                    query.Limit,
                    query.Offset,
                    query.OrganisationId,
                    query.PetId,
                    Status = (query.PetStatuses.Any() ? query.PetStatuses : DefaultPetStatuses).Select(_ => _.ToString()),
                    Genders = (query.Genders.Any() ? query.Genders : DefaultPetGenders).Select(_ => _.ToString()),
                    Types = (query.Types.Any() ? query.Types : DefaultPetTypes).Select(_ => _.ToString()),
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
            Items = (await pets.ReadAsync<PetsDto>()).Select(_ => new PetView(
                Id: _.Id,
                Name: _.Name,
                BeforePhotoLink: _.BeforePhotoLink,
                AfterPhotoLink: _.AfterPhotoLink,
                PetState: _.PetState,
                MdShortBody: _.MdShortBody,
                MdBody: _.MdBody,
                Type: _.Type,
                Gender: _.Gender,
                UpdateDate: _.UpdateDate,
                AnimalId: _.AnimalId
            ))
        };
    }
}