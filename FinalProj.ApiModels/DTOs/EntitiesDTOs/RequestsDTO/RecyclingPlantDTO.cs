using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class RecyclingPlantDTO : LocationDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Income { get; set; }

    }
}
