using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class EcoBoxDTO : BaseEntityDTO
    {
        public int ProductPrice { get; set; }
        public int WearDegree { get; set; }
    }
}
