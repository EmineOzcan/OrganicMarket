using AutoMapper;
using OrganicMarket.Api.DTO;
using OrganicMarket.Core.Models;

namespace OrganicMarket.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserDTO>();
        }
    }
}
