using FinalApp.Domain.Models.Abstractions.BaseRequests;

namespace FinalApp.Domain.Models.Entities.Requests.RequestsInfo
{
    public class RecyclingPlant : CompanyContactInfo
    {
        public int Income { get; set; }

        public ICollection<Request>? Requests { get; set; }
    }
}
