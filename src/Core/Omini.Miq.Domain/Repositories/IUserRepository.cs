
using Omini.Miq.Domain.Authentication;

namespace Omini.Miq.Domain.Repositories;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> FindByEmail(string email);
}