using AutoMapper;
using Omini.Miq.Api.Dtos;
using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Api.Profiles;

public class PromissoryMapperProfile : Profile
{
    public PromissoryMapperProfile()
    {
        CreateMap<Promissory, PromissoryOutputDto>();            
        CreateMap<PromissoryItem, PromissoryOutputDto.PromissoryItemOutputDto>();
    }
}