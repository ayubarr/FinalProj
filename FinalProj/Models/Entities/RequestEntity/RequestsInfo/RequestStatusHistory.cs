using FinalProj.Domain.Models.Abstractions.BaseEntities;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.Domain.Models.Entities.Requests.RequestsInfo
{
    public class RequestStatusHistory : BaseEntity
    {
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public Status PreviousStatus { get; set; }
        public Status NewStatus { get; set; }

        public Request? Request { get; set; }
    }
}
