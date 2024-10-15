using Login.Models.Entities;

namespace Login.Helpers.Authentication.Security;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}

