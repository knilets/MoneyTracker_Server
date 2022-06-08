using AutoMapper;
using MoneyTracker.Authentication.Entities;

namespace MoneyTracker.Authentication.ApplicationUsers;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<UserRegistrationRequest, ApplicationUser>();
    }
}