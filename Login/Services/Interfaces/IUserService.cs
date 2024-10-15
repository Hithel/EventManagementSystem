using Login.Models.Dtos.User;
using Login.Models.ViewModels.User;

namespace Login.Services.Interfaces;

public interface IUserService : IGenericService<UserVm, UserDto>
{
    Task<UserVm> GetByUsernameAsync(string username);
    Task<UserVm> GetByRefreshTokenAsync(string username);
}
