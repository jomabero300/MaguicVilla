using AutoMapper;
using MaguicVilla.Api.Models;
using MaguicVilla.Api.Models.Dto;

namespace MaguicVilla.Api
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa,VillaDto>();
            CreateMap<VillaDto,Villa>();

            CreateMap<Villa,VillaCreateDto>().ReverseMap();
            CreateMap<Villa,VillaUpdateDto>().ReverseMap();
            
        }
    }
}
