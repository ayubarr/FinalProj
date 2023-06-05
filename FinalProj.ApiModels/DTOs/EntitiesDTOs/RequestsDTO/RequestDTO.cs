using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class RequestDTO : BaseEntityDTO
    {
        public string? Comment { get; set; }
        public int BoxQuantity { get; set; }
        public DateTime CompletedTime { get; set; }
        public WorkTypes WorkType { get; set; }
        public Types RequestType { get; set; } = Types.RequestExecution;
        public Status RequestStatus { get; set; }
        public string? ClientId { get; set; }

    }
}
