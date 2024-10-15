using AutoMapper;
using Login.Models.Dtos.Authentication;
using Login.Models.Dtos.Rol;
using Login.Models.Dtos.User;
using Login.Models.Entities;
using Login.Models.ViewModels.Rol;
using Login.Models.ViewModels.User;

namespace Login.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UserVm>().ReverseMap();

        CreateMap<RolDto, Rol>().ReverseMap();
        CreateMap<Rol, RolVm>().ReverseMap();

    }
}