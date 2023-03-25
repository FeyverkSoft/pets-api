namespace Pets.DB.Migrations.Entities;

using System;

internal sealed class Organisation
{
    /// <summary>
    ///     Идентификатор организации которой принадлежит животное
    /// </summary>
    public Guid Id { get; }
}