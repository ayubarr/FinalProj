using FinalProj.Domain.Models.Abstractions.BaseRequests;
using FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo;

namespace FinalProj.Domain.Models.Entities.Requests.RequestsInfo
{
    public class Location : BaseLocation
    {
        public Request? Request { get; set; }
        public ICollection<EcoBox>? EcoBoxes { get; set; }
    }
}
