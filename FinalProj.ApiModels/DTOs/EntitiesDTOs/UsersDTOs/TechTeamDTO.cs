using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.UsersDTOs
{
    public class TechTeamDTO : BaseUserDTO
    {
        public  Roles UserType { get; set; } = Roles.TechnicalSpecialist;

    }
}
