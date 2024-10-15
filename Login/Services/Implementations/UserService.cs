using AutoMapper;
using Login.Models.Dtos.User;
using Login.Models.Entities;
using Login.Models.ViewModels.User;
using Login.Repositories.Interfaces;
using Login.Services.Interfaces;

namespace Login.Services.Implementations;

public class UserService : GenericService<UserVm, UserDto, User>, IUserService
{
    private readonly IUser _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUser userRepository, IMapper mapper)
        : base(userRepository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserVm> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return _mapper.Map<UserVm>(user);
    }

    public async Task<UserVm> GetByRefreshTokenAsync(string Username)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(Username);
        return _mapper.Map<UserVm>(user);
    }
}