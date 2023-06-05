using FinalProj.Domain.Models.Abstractions.BaseRequests;

namespace FinalProj.Domain.Models.Entities.Requests.RequestsInfo
{
    public class Review : BaseRequest
    {
        public string? ReviewText { get; set; }
        public int Evaluation { get; set; }

        public Request? Request { get; set; }
    }
}
