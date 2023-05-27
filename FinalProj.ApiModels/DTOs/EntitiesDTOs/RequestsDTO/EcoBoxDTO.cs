using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class EcoBoxDTO : BaseEntityDTO
    {
        public int ProductPrice { get; set; }
        public int WearDegree { get; set; }
    }
}
