using AutoMapper;
using MoneyTracker.Logic.Users;
using MoneyTracker.Storage.Models.Entities;

namespace MoneyTracker.Logic.Service.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}