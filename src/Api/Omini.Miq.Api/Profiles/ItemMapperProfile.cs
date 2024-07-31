using AutoMapper;
using Omini.Miq.Api.Dtos;
using Omini.Miq.Domain.Externals.Models;

namespace Omini.Miq.Api.Profiles;

public class ItemMapperProfile : Profile
{
    public ItemMapperProfile()
    {
        CreateMap<Item, ItemOutputDto>();
    }
}