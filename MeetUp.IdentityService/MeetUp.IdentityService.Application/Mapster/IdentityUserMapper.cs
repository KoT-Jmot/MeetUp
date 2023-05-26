using MeetUp.IdentityService.Application.DTOs.OutputDto;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using Microsoft.AspNetCore.Identity;
using Mapster;

namespace MeetUp.IdentityService.Application.Mapster
{
    public class IdentityUserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserForRegistrationDto, IdentityUser>();
            config.NewConfig<IdentityUser, OutputUserDto>();
        }
    }
}
