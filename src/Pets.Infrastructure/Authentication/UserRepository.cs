namespace Pets.Infrastructure.Authentication;

using System.Threading;

using Domain.Authentication;

using Microsoft.EntityFrameworkCore;

public sealed class UserRepository : IUserRepository
{
    private readonly AuthenticationDbContext _context;

    public UserRepository(AuthenticationDbContext context)
    {
        _context = context;
    }

    public async Task<User> Find(String? login, CancellationToken cancellationToken)
    {
        return await _context.Users.SingleOrDefaultAsync(_ => _.Login == login, cancellationToken);
    }

    public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users.SingleOrDefaultAsync(_ => _.Id == userId, cancellationToken);
    }
}