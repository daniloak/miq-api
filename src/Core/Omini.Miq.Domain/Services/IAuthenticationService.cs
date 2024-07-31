namespace Omini.Miq.Domain;

public interface IAuthenticationService
{
    Task<string> Register(string email, string password);
}