using FinalProj.Domain.Models.Abstractions.BaseRequests;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;

namespace FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo
{
    public class EcoBox : BaseEcoBox
    {
        public int WearDegree { get; set; }

        public Location Location { get; set; }
        public Guid? LocationId { get; set; }

        public EcoBoxTemplate Template { get; set; }
        public Guid? TemplateId { get; set; }
    }
}
