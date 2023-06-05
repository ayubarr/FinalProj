using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.UsersDTOs
{
    public class ClientDTO : BaseUserDTO
    {
        public  Roles UserType { get; set; } = Roles.Client;
    }
}
