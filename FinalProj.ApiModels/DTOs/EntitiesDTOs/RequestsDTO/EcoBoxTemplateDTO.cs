using FinalProj.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class EcoBoxTemplateDTO : BaseEntityDTO
    {
        public Materials MaterialType { get; set; }
        public TrashTypes TrashType { get; set; }
        public string Capacity { get; set; }

    }
}
