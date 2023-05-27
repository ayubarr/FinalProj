using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO
{
    public class EcoBoxTemplateDTO : BaseEntityDTO
    {
        public Materials MaterialType { get; set; }
        public TrashTypes TrashType { get; set; }
        public string Capacity { get; set; }
        public int? SupplierId { get; set; }

    }
}
