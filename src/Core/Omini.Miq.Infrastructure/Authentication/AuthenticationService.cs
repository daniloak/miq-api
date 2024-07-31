using FirebaseAdmin.Auth;
using Omini.Miq.Domain;

internal sealed class AuthenticationService : IAuthenticationService
{
    public async Task<string> Register(string email, string password)
    {
        var userArgs = new UserRecordArgs(){
            Email = email,
            Password = password
        };

        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        return userRecord.Uid;
    }
}