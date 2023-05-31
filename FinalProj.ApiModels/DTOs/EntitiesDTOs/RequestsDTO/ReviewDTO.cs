using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class ReviewDTO : BaseEntityDTO
    {
        public DateTime RequestCreatedTime { get; set; } = DateTime.UtcNow;
        public string? ReviewText { get; set; }
        public int Evaluation { get; set; }
    }
}
