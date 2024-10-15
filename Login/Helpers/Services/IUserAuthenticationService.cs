using Login.Models.Dtos.Authentication;
using Login.Models.ViewModels.Authentication;

namespace Login.Helpers.Services;

public interface IUserAuthenticationService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DataUserDto> GetTokenAsync(LoginDto model);
    Task<string> AddRoleAsync(AddRoleDto model);
    Task<DataUserDto> RefreshTokenAsync(string refreshToken);
    Task<DataUserDto> LoginAsync(LoginDto Dto);
    Task<bool> VerifyAsync(AuthDto dto);
}
