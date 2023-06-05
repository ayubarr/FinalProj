using FinalProj.Domain.Models.Abstractions.BaseRequests;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo
{
    public class EcoBoxTemplate : BaseEcoBox
    {
        public Materials MaterialType { get; set; }
        public TrashTypes TrashType { get; set; }
        public string Capacity { get; set; }

        public ICollection<EcoBox>? EcoBoxes { get; set; }

        public SupplierCompany SupplierCompany { get; set; }
        public Guid? SupplierId { get; set; } 
    }
}
