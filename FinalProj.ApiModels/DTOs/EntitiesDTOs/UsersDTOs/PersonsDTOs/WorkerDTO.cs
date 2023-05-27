using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs.PersonsDTO
{
    public class WorkerDTO : BaseEntityDTO
    {
        public string Salary { get; set; }
        public DateTime HireTime { get; set; }
        public Roles Position { get; set; } = Roles.TechnicalWorker;

        public int? TechTeamId { get; set; }
    }
}
