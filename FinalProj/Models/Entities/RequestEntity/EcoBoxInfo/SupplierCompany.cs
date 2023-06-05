using FinalProj.Domain.Models.Abstractions.BaseRequests;

namespace FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo
{
    public class SupplierCompany : CompanyContactInfo
    {
        public int MaterialPrice { get; set; }

        public ICollection<EcoBoxTemplate>? EcoBoxTemplates { get; set; }
    }
}
