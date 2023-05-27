using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs
{
    public class SupportOperatorDTO : BaseUserDTO
    {
        public  Roles UserType { get; set; } = Roles.TechnicalSupportOperator;
    }
}
