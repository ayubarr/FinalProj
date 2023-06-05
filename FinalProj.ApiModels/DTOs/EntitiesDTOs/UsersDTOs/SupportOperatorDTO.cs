using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.UsersDTOs
{
    public class SupportOperatorDTO : BaseUserDTO
    {
        public  Roles UserType { get; set; } = Roles.TechnicalSupportOperator;
    }
}
