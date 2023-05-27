using FinalApp.Domain.Models.Abstractions.BaseRequests;
using FinalApp.Domain.Models.Entities.Requests.EcoBoxInfo;

namespace FinalApp.Domain.Models.Entities.Requests.RequestsInfo
{
    public class Location : BaseLocation
    {
        public Request? Request { get; set; }
        public ICollection<EcoBox>? EcoBoxes { get; set; }
    }
}
