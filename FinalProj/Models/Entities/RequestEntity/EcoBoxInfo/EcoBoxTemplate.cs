using FinalApp.Domain.Models.Abstractions.BaseRequests;
using FinalApp.Domain.Models.Enums;

namespace FinalApp.Domain.Models.Entities.Requests.EcoBoxInfo
{
    public class EcoBoxTemplate : BaseEcoBox
    {
        public Materials MaterialType { get; set; }
        public TrashTypes TrashType { get; set; }
        public string Capacity { get; set; }

        public ICollection<EcoBox>? EcoBoxes { get; set; }

        public SupplierCompany SupplierCompany { get; set; }
        public int? SupplierId { get; set; } 
    }
}
