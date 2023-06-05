using AutoMapper;
using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalProj.Domain.Models.Abstractions.BaseUsers;
using FinalProj.Domain.Models.Entities.Persons.Users;

namespace FinalProj.Services.Mapping.UsersMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, BaseUserDTO>();
            CreateMap<Client, ClientDTO>();
            CreateMap<TechTeam, TechTeamDTO>();
            CreateMap<SupportOperator, SupportOperatorDTO>();
        }
    }
}
