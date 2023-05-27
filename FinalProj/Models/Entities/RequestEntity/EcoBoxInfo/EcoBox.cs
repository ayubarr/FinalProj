using FinalApp.Domain.Models.Abstractions.BaseRequests;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;

namespace FinalApp.Domain.Models.Entities.Requests.EcoBoxInfo
{
    public class EcoBox : BaseEcoBox
    {
        public int WearDegree { get; set; }

        public Location Location { get; set; }
        public int? LocationId { get; set; }

        public EcoBoxTemplate Template { get; set; }
        public int? TemplateId { get; set; }
    }
}
