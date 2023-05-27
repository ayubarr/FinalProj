using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs
{
    public class TechTeamDTO : BaseUserDTO
    {
        public  Roles UserType { get; set; } = Roles.TechnicalSpecialist;
        public int? WorkerId { get; set; }

    }
}
