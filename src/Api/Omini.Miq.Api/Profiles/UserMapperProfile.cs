using AutoMapper;
using Omini.Miq.Api.Dtos;
using Omini.Miq.Domain.Authentication;

namespace Omini.Miq.Api.Profiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserOutputDto>();            
    }
}