using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.UsersDTOs.PersonsDTO
{
    public class WorkerDTO : BaseEntityDTO
    {
        public string Salary { get; set; }
        public DateTime HireTime { get; set; }
        public Roles Position { get; set; }

    }
}
