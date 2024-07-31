using Microsoft.EntityFrameworkCore;
using Omini.Miq.Domain.Authentication;
using Omini.Miq.Domain.Repositories;

namespace Omini.Miq.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly MiqContext _context;

    public UserRepository(MiqContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> FindByEmail(string email)
    {
        return await _context.Users.AsNoTracking().SingleOrDefaultAsync(p => p.Email == email);
    }
}
